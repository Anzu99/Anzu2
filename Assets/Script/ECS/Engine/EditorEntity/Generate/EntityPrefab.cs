
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

    [ColoredFoldoutGroup(Component.TransformComponent, .5f, .5f, .5f, 1), HideLabel, PropertyOrder(0)]
    [ShowIf("isShowTransformComponent", true), ShowInInspector]
    private TransformComponent TransformComponent;

    [ColoredFoldoutGroup(Component.MoveComponent, .5f, .5f, .5f, 1), HideLabel, PropertyOrder(1)]
    [ShowIf("isShowMoveComponent", true), ShowInInspector]
    private MoveComponent MoveComponent;

    [ColoredFoldoutGroup(Component.InputComponent, .5f, .5f, .5f, 1), HideLabel, PropertyOrder(2)]
    [ShowIf("isShowInputComponent", true), ShowInInspector]
    private InputComponent InputComponent;


    private bool isShowTransformComponent = false;
    private bool isShowMoveComponent = false;
    private bool isShowInputComponent = false;
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
        Component.TransformComponent,
        Component.MoveComponent,
        Component.InputComponent,
    };
    public static object entityPrefabCache;

    public partial void CopyComponent(Component component)
    {
        switch (component)
        {
            case Component.TransformComponent:
                entityPrefabCache = TransformComponent;
                break;
            case Component.MoveComponent:
                entityPrefabCache = MoveComponent;
                break;
            case Component.InputComponent:
                entityPrefabCache = InputComponent;
                break;
        }
    }

    public partial void PasteComponent(Component component)
    {
        switch (component)
        {
            case Component.TransformComponent:
                TransformComponent = (TransformComponent)entityPrefabCache;
                break;
            case Component.MoveComponent:
                MoveComponent = (MoveComponent)entityPrefabCache;
                break;
            case Component.InputComponent:
                InputComponent = (InputComponent)entityPrefabCache;
                break;
        }
    }

    public partial void RemoveComponent(Component component)
    {
        flags.Remove(component);
        switch (component)
        {
            case Component.TransformComponent:
                isShowTransformComponent = false;
                break;
            case Component.MoveComponent:
                isShowMoveComponent = false;
                break;
            case Component.InputComponent:
                isShowInputComponent = false;
                break;
        }
        EditorUtility.SetDirty(this);
    }

    public partial void AddComponent(Component component)
    {
        if (flags.Contains(component)) return;
        switch (component)
        {
            case Component.TransformComponent:
                isShowTransformComponent = true;
                break;
            case Component.MoveComponent:
                isShowMoveComponent = true;
                break;
            case Component.InputComponent:
                isShowInputComponent = true;
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
                case Component.TransformComponent:
                    componentDatas.Add(TransformComponent);
                    break;
                case Component.MoveComponent:
                    componentDatas.Add(MoveComponent);
                    break;
                case Component.InputComponent:
                    componentDatas.Add(InputComponent);
                    break;
            }
        }

        ComponentEditorData componentEditorData = new ComponentEditorData();
        ComponentConfigData componentConfigData = ColoredFoldoutGroupAttributeDrawer.componentConfigData;
        componentEditorData.componentConfigData = componentConfigData;
        componentEditorData.Save();
    }


    public void CheckShowComponent()
    {
        flags ??= new List<Component>();
        for (var i = 0; i < flags.Count; i++)
        {
            switch (flags[i])
            {
                case Component.TransformComponent:
                    isShowTransformComponent = true;
                    TransformComponent = (TransformComponent)componentDatas[i];
                    break;
                case Component.MoveComponent:
                    isShowMoveComponent = true;
                    MoveComponent = (MoveComponent)componentDatas[i];
                    break;
                case Component.InputComponent:
                    isShowInputComponent = true;
                    InputComponent = (InputComponent)componentDatas[i];
                    break;
            }
        }
    }

}
