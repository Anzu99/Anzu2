
using Sirenix.OdinInspector;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{
    
    [ColoredFoldoutGroup(Component.MoveComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowMoveComponent", true), ShowInInspector]
    public MoveComponent MoveComponent
    {
        get => isShowMoveComponent ? entity.GetComponent<MoveComponent>(Component.MoveComponent) : default;
        set => entity.GetComponent<MoveComponent>(Component.MoveComponent) = value;
    }

    [ColoredFoldoutGroup(Component.TransformComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(1)]
    [ShowIf("isShowTransformComponent", true), ShowInInspector]
    public TransformComponent TransformComponent
    {
        get => isShowTransformComponent ? entity.GetComponent<TransformComponent>(Component.TransformComponent) : default;
        set => entity.GetComponent<TransformComponent>(Component.TransformComponent) = value;
    }

    
    bool isShowMoveComponent => HasComponent(Component.MoveComponent);
    bool isShowTransformComponent => HasComponent(Component.TransformComponent);
    
    // public partial void SetUpDataPrefab(EntityPrefab entityPrefab)
    // {
    //     for (var i = 0; i < entityPrefab.flags.Count; i++)
    //     {
    //         SetData(i);
    //     }
    //     Destroy(entityPrefab);
    //     void SetData(int index)
    //     {
    //         switch (entityPrefab.flags[index])
    //         {
                
    //             case Component.MoveComponent:
    //                 MoveComponent _MoveComponent = (MoveComponent)entityPrefab.componentDatas[index];
    //                 _MoveComponent.IdEntity = IdEntity;
    //                 MoveComponent = _MoveComponent;
    //                 break;
    //             case Component.TransformComponent:
    //                 TransformComponent _TransformComponent = (TransformComponent)entityPrefab.componentDatas[index];
    //                 _TransformComponent.IdEntity = IdEntity;
    //                 TransformComponent = _TransformComponent;
    //                 break;
    //         }
    //     }
    // }
        
}
