using UnityEngine;

public class InputSystem : SystemBase
{
    // EntityQuery entityQuery2;
    // public override void Start()
    // {
    //     entityQuery = new EntityQuery(Component.InputComponent);
    //     entityQuery2 = new EntityQuery(Component.SpeedComponent);
    // }

    // public override void Execute()
    // {
    //     ForEach((ref InputComponent input) =>
    //     {
    //         var idEntity = input.IdEntity;
    //         if (Input.GetKeyDown(KeyCode.A))
    //         {
    //             if (!input.HasSpeedComponent)
    //             {
    //                 input.HasSpeedComponent = true;
    //                 SpeedComponent speedComponent = World.GetEntity(idEntity).AddComponent<SpeedComponent>(Component.SpeedComponent);
    //                 speedComponent.speed = 1;
    //             }
    //         }
    //         if (Input.GetKeyDown(KeyCode.R))
    //         {
    //             if (input.HasSpeedComponent)
    //             {
    //                 input.HasSpeedComponent = false;
    //                 World.GetEntity(idEntity).RemoveComponent(Component.SpeedComponent);
    //             }

    //         }
    //     });
    }
// }
