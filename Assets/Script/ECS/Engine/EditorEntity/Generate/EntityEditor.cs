
using Sirenix.OdinInspector;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{
    
    [ColoredFoldoutGroup(Component.TransformComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowTransformComponent", true), ShowInInspector]
    public TransformComponent TransformComponent
    {
        get => isShowTransformComponent ? entity.GetComponent<TransformComponent>(Component.TransformComponent) : default;
        set => entity.GetComponent<TransformComponent>(Component.TransformComponent) = value;
    }

    [ColoredFoldoutGroup(Component.MoveComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(1)]
    [ShowIf("isShowMoveComponent", true), ShowInInspector]
    public MoveComponent MoveComponent
    {
        get => isShowMoveComponent ? entity.GetComponent<MoveComponent>(Component.MoveComponent) : default;
        set => entity.GetComponent<MoveComponent>(Component.MoveComponent) = value;
    }

    [ColoredFoldoutGroup(Component.InputComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(2)]
    [ShowIf("isShowInputComponent", true), ShowInInspector]
    public InputComponent InputComponent
    {
        get => isShowInputComponent ? entity.GetComponent<InputComponent>(Component.InputComponent) : default;
        set => entity.GetComponent<InputComponent>(Component.InputComponent) = value;
    }

    
    bool isShowTransformComponent => HasComponent(Component.TransformComponent);
    bool isShowMoveComponent => HasComponent(Component.MoveComponent);
    bool isShowInputComponent => HasComponent(Component.InputComponent);
    
}
