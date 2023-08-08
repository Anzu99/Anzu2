using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : SystemBase
{
    public override void Start()
    {
        entityQuery = new EntityQuery(Component.TransformComponent, Component.MoveComponent);
    }
    public override void Execute()
    {
        NormalAction((ref TransformComponent transformComponent, ref MoveComponent moveComponent) =>
        {
            transformComponent.transform.localPosition = Vector3.up * Time.deltaTime;
        });
    }
}


