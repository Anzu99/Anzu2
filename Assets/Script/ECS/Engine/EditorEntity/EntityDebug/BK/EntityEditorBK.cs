// using Sirenix.OdinInspector;
// using UnityEngine;

// public partial class EntityEditor : MonoBehaviour
// {
//     private ushort idEntity;
//     public ushort IdEntity { get => idEntity; set => idEntity = value; }
//     private Component flagsComponent;
//     public void OnUpdate()
//     {
//         UpdateComponentFlags();
//     }

//     void UpdateComponentFlags()
//     {
//         flagsComponent = World.GetEntity(IdEntity).flagsComponent;
//     }

//     public bool HasComponent(Component flags, Component flagsComponent)
//     {
//         return (flagsComponent & flags) != 0;
//     }
//     public bool CompareComponentArray(Component flags)
//     {
//         return (flagsComponent & flags) == flags;
//     }

//     public partial void SetUpDataPrefab(EntityPrefab entityPrefab);

// }


// /* =========================================== GENERATE PROPERTIES =========================================== */

// public partial class EntityEditor : MonoBehaviour
// {
//     [ColoredFoldoutGroup(Component.Velocity, 0, 1, 0, 1), HideLabel, PropertyOrder(1)]
//     [ShowIf("isShowVelocity", true), ShowInInspector]
//     public VelocityComponent VelocityComponent
//     {
//         get => isShowVelocity ? World.GetEntity(IdEntity).GetComponent<VelocityComponent>(Component.Velocity) : default;
//         set => World.GetEntity(IdEntity).GetComponent<VelocityComponent>(Component.Velocity) = value;
//     }

//     [ColoredFoldoutGroup(Component.Transform, 0, 0, 1, 1), HideLabel, PropertyOrder(2)]
//     [ShowIf("isTransformComponent", true), ShowInInspector]
//     public TransformComponent TransformComponent
//     {
//         get => isTransformComponent ? World.GetEntity(IdEntity).GetComponent<TransformComponent>(Component.Transform) : default;
//         set => World.GetEntity(IdEntity).GetComponent<TransformComponent>(Component.Transform) = value;
//     }

//     [ColoredFoldoutGroup(Component.Speed, 0, 1, 0, 1), HideLabel, PropertyOrder(3)]
//     [ShowIf("isSpeedComponent", true), ShowInInspector]
//     public SpeedComponent SpeedComponent
//     {
//         get => isSpeedComponent ? World.GetEntity(IdEntity).GetComponent<SpeedComponent>(Component.Speed) : default;
//         set => World.GetEntity(IdEntity).GetComponent<SpeedComponent>(Component.Speed) = value;
//     }

//     [ColoredFoldoutGroup(Component.Input, 0, 0, 1, 1), HideLabel, PropertyOrder(4)]
//     [ShowIf("isInputComponent", true), ShowInInspector]
//     public InputComponent InputComponent
//     {
//         get => isInputComponent ? World.GetEntity(IdEntity).GetComponent<InputComponent>(Component.Input) : default;
//         set => World.GetEntity(IdEntity).GetComponent<InputComponent>(Component.Input) = value;
//     }

//     bool isShowVelocity => HasComponent(Component.Velocity, flagsComponent);
//     bool isTransformComponent => HasComponent(Component.Transform, flagsComponent);
//     bool isSpeedComponent => HasComponent(Component.Speed, flagsComponent);
//     bool isInputComponent => HasComponent(Component.Input, flagsComponent);

//     public partial void SetUpDataPrefab(EntityPrefab entityPrefab)
//     {
//         foreach (var item in entityPrefab.flags)
//         {
//             SetData(item);
//         }

//         Destroy(entityPrefab);
//         void SetData(Component item)
//         {
//             switch (item)
//             {
//                 case Component.Velocity:
//                     entityPrefab.VelocityComponent.IdEntity = IdEntity;
//                     VelocityComponent = entityPrefab.VelocityComponent;
//                     break;
//                 case Component.Transform:
//                     entityPrefab.TransformComponent.IdEntity = IdEntity;
//                     TransformComponent = entityPrefab.TransformComponent;
//                     break;
//                 case Component.Speed:
//                     entityPrefab.SpeedComponent.IdEntity = IdEntity;
//                     SpeedComponent = entityPrefab.SpeedComponent;
//                     break;
//                 case Component.Input:
//                     entityPrefab.InputComponent.IdEntity = IdEntity;
//                     InputComponent = entityPrefab.InputComponent;
//                     break;
//             }
//         }
//     }

//     /* =========================================== GENERATE PROPERTIES =========================================== */
// }