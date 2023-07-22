using Sirenix.OdinInspector;
using UnityEngine;

public partial class EntityEditor : MonoBehaviour
{
    private ushort idEntity;
    public ushort IdEntity { get => idEntity; set => idEntity = value; }
    private Flag flag;
    public void OnUpdate()
    {
        UpdateComponentFlags();
    }

    void UpdateComponentFlags()
    {
        flag = World.GetEntity(IdEntity).flag;
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

    public partial void SetUpDataPrefab(EntityPrefab entityPrefab);

}