using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
public class Entity
{
    public ushort idEntity;
    public Archetype archetype;
    public GameObject gameObject;
    public bool IsAlive;
    public string Name
    {
        get { return gameObject.name; }
        set { gameObject.name = value; }
    }

    public Entity(Archetype archetype, ushort _idEntity, GameObject go)
    {
        idEntity = _idEntity;
        gameObject = go;
        this.archetype = archetype;
        archetype.AddEntity(this);
    }

    public ref T GetComponent<T>(Component component) where T : struct, IComponentData
    {
        return ref archetype.GetComponent<T>(component, idEntity);
    }

    public void SetComponent<T>(T value, Component component) where T : struct, IComponentData
    {

    }

    public void AddComponent(Component component, ushort chunkSize = 0)
    {
        World.AddEngineUpdateAction(Action);
        void Action()
        {
            Flag newFlag = new Flag();
            newFlag.Copy(archetype.flag);
            newFlag.AddComponent(component);
            World.archetypeManager.EntityChangeArchetype(this, newFlag, chunkSize);
        }
    }

    public void RemoveComponent(Component component, ushort chunkSize = 0)
    {
        World.AddEngineUpdateAction(Action);
        void Action()
        {
            Flag newFlag = new Flag();
            newFlag.Copy(archetype.flag);
            newFlag.RemoveComponent(component);
            World.archetypeManager.EntityChangeArchetype(this, newFlag, chunkSize);
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        
    }

    public void DestroyEntity()
    {
        World.AddEngineUpdateAction(Action);
        void Action()
        {
            archetype.RemoveEntity(idEntity);
            Object.Destroy(gameObject);
        }
    }

    public Entity Instance()
    {
        Entity entity = null;
        return entity;
    }
}