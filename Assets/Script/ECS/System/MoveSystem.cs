using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : SystemBase
{
    public override void Start()
    {
        entityQuery = new EntityQuery(Component.InfoComponent, Component.MovementComponent);
    }
    private float waveSpeed = 5;
    private float cycle;

    float deltaTime;
    public override void Execute()
    {
        deltaTime = Time.deltaTime;
        cycle += waveSpeed * deltaTime;
        float sinval = Mathf.Sin(cycle);
        ForEach((ref InfoComponent infoComponent, ref MovementComponent movementComponent) =>
        {
            if (!movementComponent.transform) return;
            if (infoComponent.direction == InfoComponent.Direction.Horizontal)
            {
                if (infoComponent.invert)
                {
                    movementComponent.transform.position += Vector3.left * deltaTime * infoComponent.speed + Vector3.up * sinval * deltaTime;
                    if (movementComponent.transform.position.x <= infoComponent.xRange.x)
                    {
                        infoComponent.invert = false;
                    }
                }
                else
                {
                    movementComponent.transform.position += Vector3.right * deltaTime * infoComponent.speed + Vector3.up * sinval * deltaTime;
                    if (movementComponent.transform.position.x >= infoComponent.xRange.y)
                    {
                        infoComponent.invert = true;
                    }
                }
            }
            else
            {
                if (infoComponent.invert)
                {
                    movementComponent.transform.position += Vector3.down * deltaTime * infoComponent.speed + Vector3.right * sinval * deltaTime;
                    if (movementComponent.transform.position.y <= infoComponent.yRange.x)
                    {
                        infoComponent.invert = false;
                    }
                }
                else
                {
                    movementComponent.transform.position += Vector3.up * deltaTime * infoComponent.speed + Vector3.right * sinval * deltaTime; ;
                    if (movementComponent.transform.position.y >= infoComponent.yRange.y)
                    {
                        infoComponent.invert = true;
                    }
                }
            }
        });
    }
}
