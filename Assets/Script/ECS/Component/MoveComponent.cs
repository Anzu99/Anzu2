using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MoveComponent : IComponentData
{
    private Entity entity;
    public float speed;
    private float speed1;
    private float speed2;
    private float speed3;
    private float speed4;

    public Vector3 pos;
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