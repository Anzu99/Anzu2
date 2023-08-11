
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct InputComponent : IComponentData
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