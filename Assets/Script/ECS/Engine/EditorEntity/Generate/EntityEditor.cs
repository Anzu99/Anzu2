
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{

    [ColoredFoldoutGroup(Component.TransformComponent, 0, 1, 0, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowTransformComponent", true), ShowInInspector]
    public TransformComponent TransformComponent
    {
        get
        {
            ComponentEditor<TransformComponent> componentEditor = entity.GetComponentArrayEditor<TransformComponent>(Component.TransformComponent);
            return componentEditor.GetComponentData();
        }
        set
        {
            ComponentEditor<TransformComponent> componentEditor = entity.GetComponentArrayEditor<TransformComponent>(Component.TransformComponent);
            componentEditor.array[componentEditor.index] = value;
        }
    }


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

    //             case Component.TransformComponent:
    //                 TransformComponent _TransformComponent = (TransformComponent)entityPrefab.componentDatas[index];
    //                 _TransformComponent.IdEntity = IdEntity;
    //                 TransformComponent = _TransformComponent;
    //                 break;
    //         }
    //     }
    // }
}

public class ComponentEditor<T> where T : struct, IComponentData
{
    public NativeArray<T> array;
    public int index;

    public T GetComponentData()
    {
        return array[index];
    }
}
