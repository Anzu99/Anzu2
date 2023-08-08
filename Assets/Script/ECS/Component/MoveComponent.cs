using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MoveComponent : IComponentData
{
    private Entity entity;
    public Entity GetEntity()
    {
        return entity;
    }

    public void SetEntity(Entity value)
    {
        this.entity = value;
    }

    public void Start()
    {
    }
}