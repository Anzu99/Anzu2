using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Entity
{
    public ushort idEntity;
    public Flag flag;
    public GameObject gameObject;
    public bool IsAlive;
    public string Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value; }
    }

    public Entity(Flag flag, ushort _idEntity, GameObject go)
    {
        idEntity = _idEntity;
        gameObject = go;
        this.flag = flag;

    }

    public Entity(ushort _idEntity, EntityPrefab entityPrefab)
    {
        idEntity = _idEntity;
        gameObject = entityPrefab.gameObject;
        flag.Init();
        foreach (var item in entityPrefab.flags)
        {
            AddComponentInWorld(item);
        }
    }

    public void CreateComponent(Component[] _Components, ushort _idEntity)
    {
        idEntity = _idEntity;
        foreach (var item in _Components)
        {
            AddComponentInWorld(item);
        }
    }

    private void AddComponentInWorld(Component component)
    {
        flag.AddComponent(component);
        World.componentManager.CreateComponents(component, idEntity);
    }

    public void AddComponent(Component component)
    {
        Flag newflag = flag;
        newflag.AddComponent(component);
        World.archetypeManager.ChangeEntityArchetype(this, newflag);
        flag.AddComponent(component);
        World.componentManager.CreateComponents(component, idEntity);
    }
    public ref T AddComponent<T>(Component component) where T : struct, IComponentData
    {
        Flag newflag = flag;
        newflag.AddComponent(component);
        World.archetypeManager.ChangeEntityArchetype(this, newflag);
        flag.AddComponent(component);
        World.componentManager.CreateComponents(component, idEntity);
        return ref GetComponent<T>(component);
    }
    public void RemoveComponent(Component component)
    {
        Flag newflag = flag;
        newflag.RemoveComponent(component);
        World.archetypeManager.ChangeEntityArchetype(this, newflag);
        flag.RemoveComponent(component);
        World.componentManager.RemoveComponent(component, idEntity);
    }

    public ref T GetComponent<T>(Component flag) where T : struct, IComponentData
    {
        return ref World.componentManager.GetComponent<T>(flag, idEntity);
    }
    public ref T GetComponent<T>(Component component) where T : struct, IComponentData
    {
        return ref World.archetypeManager.GetArchetype(flag).GetComponentArrayData<T>(component)
    }
}