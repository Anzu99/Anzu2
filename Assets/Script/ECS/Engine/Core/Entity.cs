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
}