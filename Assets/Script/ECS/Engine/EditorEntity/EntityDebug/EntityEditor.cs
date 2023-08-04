using Sirenix.OdinInspector;
using UnityEngine;

public partial class EntityEditor : MonoBehaviour
{
    private Entity entity;
    public Entity Entity { get => entity; set => entity = value; }
    private Flag flag;
    public void OnUpdate()
    {
        UpdateComponentFlags();
    }

    void UpdateComponentFlags()
    {
        flag = entity.archetype.flag;
    }

    public bool HasComponent(Component component)
    {
        if (flag.components != null) return flag.ContainComponent(component);
        return false;
    }
    public bool CompareComponentArray(Flag otherFlag)
    {
        return flag.Equal(otherFlag);
    }

    // public partial void SetUpDataPrefab(EntityPrefab entityPrefab);

}