using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TransformComponent : IComponentData
{
    public Transform transform;
    private Entity entity;
    public Entity GetEntity()
    {
        return entity;
    }

    public void SetEntity(Entity value)
    {
        entity = value;
    }

    public void Start()
    {
        transform = entity.gameObject.transform;
    }
}

