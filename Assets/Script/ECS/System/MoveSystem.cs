using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : SystemBase
{
    ActionRR<TransformComponent, MoveComponent> actionRR, actionRR2;
    public override void Start()
    {
        entityQuery = new EntityQuery(Component.TransformComponent, Component.MoveComponent);
        actionRR = (ref TransformComponent transformComponent, ref MoveComponent moveComponent) =>
        {
            moveComponent.pos = Vector3.up * deltaTime * moveComponent.speed;
        };

        actionRR2 = (ref TransformComponent transformComponent, ref MoveComponent moveComponent) =>
        {
            transformComponent.transform.position += moveComponent.pos;
        };
    }
    public override void Execute()
    {
        ParallelChunkAction(actionRR, actionRR2);
        // NormalAction(actionRR2);
    }
}


