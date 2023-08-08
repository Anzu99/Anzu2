using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : SystemBase
{
    Action2P<TransformComponent, MoveComponent> actionRR, actionRR2;
    public override void Start()
    {
        entityQuery = new EntityQuery(Component.TransformComponent, Component.MoveComponent);
        actionRR = (TransformComponent transformComponent, MoveComponent moveComponent) =>
        {
            for (int i = 0; i < 200; i++)
            {
                moveComponent.speed = 21;
                moveComponent.speed = Mathf.Pow(moveComponent.speed, 10) / 10000000;
            }
            moveComponent.pos = Vector3.up * deltaTime * moveComponent.speed;
        };

        actionRR2 = (TransformComponent transformComponent, MoveComponent moveComponent) =>
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


