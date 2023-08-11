
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InputSystem : SystemBase
{
    public override void Start()
    {
        entityQuery = new EntityQuery(Component.InputComponent);
    }

    public override void Execute()
    {
        NormalAction((ref InputComponent inputComponent) =>
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputComponent.GetEntity().AddComponent(Component.MoveComponent, 15);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                inputComponent.GetEntity().RemoveComponent(Component.InputComponent, 15);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                inputComponent.GetEntity().DestroyEntity();
            }
        });
    }
}