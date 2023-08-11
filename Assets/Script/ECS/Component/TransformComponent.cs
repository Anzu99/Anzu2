using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial struct TransformComponent : IComponentData
{
    private Entity entity;
    public readonly Entity GetEntity() => entity;
    public void SetEntity(Entity value) => entity = value;

    public Transform transform;
    public void Start()
    {
        transform = entity.gameObject.transform;
    }
}