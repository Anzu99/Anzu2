
using Sirenix.OdinInspector;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{
    
    [ColoredFoldoutGroup(Component.MovementComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowMovementComponent", true), ShowInInspector]
    public MovementComponent MovementComponent
    {
        get => isShowMovementComponent ? World.GetEntity(IdEntity).GetComponent<MovementComponent>(Component.MovementComponent) : default;
        set => World.GetEntity(IdEntity).GetComponent<MovementComponent>(Component.MovementComponent) = value;
    }

    [ColoredFoldoutGroup(Component.InfoComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(1)]
    [ShowIf("isShowInfoComponent", true), ShowInInspector]
    public InfoComponent InfoComponent
    {
        get => isShowInfoComponent ? World.GetEntity(IdEntity).GetComponent<InfoComponent>(Component.InfoComponent) : default;
        set => World.GetEntity(IdEntity).GetComponent<InfoComponent>(Component.InfoComponent) = value;
    }

    
    bool isShowMovementComponent => HasComponent(Component.MovementComponent);
    bool isShowInfoComponent => HasComponent(Component.InfoComponent);
    
    public partial void SetUpDataPrefab(EntityPrefab entityPrefab)
    {
        for (var i = 0; i < entityPrefab.flags.Count; i++)
        {
            SetData(i);
        }
        Destroy(entityPrefab);
        void SetData(int index)
        {
            switch (entityPrefab.flags[index])
            {
                
                case Component.MovementComponent:
                    MovementComponent _MovementComponent = (MovementComponent)entityPrefab.componentDatas[index];
                    _MovementComponent.IdEntity = IdEntity;
                    MovementComponent = _MovementComponent;
                    break;
                case Component.InfoComponent:
                    InfoComponent _InfoComponent = (InfoComponent)entityPrefab.componentDatas[index];
                    _InfoComponent.IdEntity = IdEntity;
                    InfoComponent = _InfoComponent;
                    break;
            }
        }
    }
        
}
