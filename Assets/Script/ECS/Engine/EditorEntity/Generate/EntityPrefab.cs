
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public partial class EntityPrefab : SerializedMonoBehaviour
{
    private void OnEnable()
    {
        CheckShowComponent();
    }
        
    [ColoredFoldoutGroup(Component.MovementComponent, .5f, .5f, .5f, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowMovementComponent", true), ShowInInspector]
    private MovementComponent MovementComponent;
    
    [ColoredFoldoutGroup(Component.InfoComponent, .5f, .5f, .5f, 1), HideLabel, PropertyOrder(1)]
    [ShowIf("isShowInfoComponent", true), ShowInInspector]
    private InfoComponent InfoComponent;
    
    
    private bool isShowMovementComponent = false;
    private bool isShowInfoComponent = false;
    [HideInInspector] public List<Component> flags;
    [HideInInspector] public List<object> componentDatas;
    
    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(100)]
    private void AddComponent()
    {
        isShowListComponent = !isShowListComponent;
    }

    bool isShowListComponent = false;
    [ValueDropdown("ListComponent", DropdownHeight = 200)]
    [ShowIf("isShowListComponent"), PropertySpace, HideLabel, PropertyOrder(1000)]
    [OnValueChanged("OnChangeValueSellectAddComponent")]
    public Component sellectedAddComponent = Component.None;
    public void OnChangeValueSellectAddComponent()
    {
        if (sellectedAddComponent != Component.None)
        {
            AddComponent(sellectedAddComponent);
            sellectedAddComponent = Component.None;
        }
        isShowListComponent = false;
    }
        
    
    public static Component[] ListComponent = new Component[]
    {
        Component.None,
        Component.MovementComponent,
        Component.InfoComponent,
    };
    public static object entityPrefabCache;
    
    public partial void CopyComponent(Component component)
    {
        switch (component)
        {
            case Component.MovementComponent:
                entityPrefabCache = MovementComponent;
                break;
            case Component.InfoComponent:
                entityPrefabCache = InfoComponent;
                break;
        }
    }
    
    public partial void PasteComponent(Component component)
    {
        switch (component)
        {
            case Component.MovementComponent:
                MovementComponent = (MovementComponent)entityPrefabCache;
                break;
            case Component.InfoComponent:
                InfoComponent = (InfoComponent)entityPrefabCache;
                break;
        }
    }
    
    public partial void RemoveComponent(Component component)
    {
        flags.Remove(component);
        switch (component)
        {
            case Component.MovementComponent:
                isShowMovementComponent = false;
                break;
            case Component.InfoComponent:
                isShowInfoComponent = false;
                break;
        }
        EditorUtility.SetDirty(this);
    }
    
    public partial void AddComponent(Component component)
    {
        if (flags.Contains(component)) return;
        switch (component)
        {
            case Component.MovementComponent:
                isShowMovementComponent = true;
                break;
            case Component.InfoComponent:
                isShowInfoComponent = true;
                break;
        }
        flags.Add(component);
        EditorUtility.SetDirty(this);
    }
    
    public void SaveData()
    {
        componentDatas = new List<object>();
        foreach (var item in flags)
        {
            switch (item)
            {
                case Component.MovementComponent:
                    componentDatas.Add(MovementComponent);
                    break;
                case Component.InfoComponent:
                    componentDatas.Add(InfoComponent);
                    break;
            }
        }
    }
    
    
    public void CheckShowComponent()
    {
        flags ??= new List<Component>();
        for (var i = 0; i < flags.Count; i++)
        {
            switch (flags[i])
            {
                case Component.MovementComponent:
                    isShowMovementComponent = true;
                    MovementComponent = (MovementComponent)componentDatas[i];
                    break;
                case Component.InfoComponent:
                    isShowInfoComponent = true;
                    InfoComponent = (InfoComponent)componentDatas[i];
                    break;
            }
        }
    }
    
}
