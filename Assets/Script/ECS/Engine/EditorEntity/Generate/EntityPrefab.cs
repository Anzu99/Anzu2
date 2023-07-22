
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
    };
    public static object entityPrefabCache;
    
    public partial void CopyComponent(Component component)
    {
        switch (component)
        {
        }
    }
    
    public partial void PasteComponent(Component component)
    {
        switch (component)
        {
        }
    }
    
    public partial void RemoveComponent(Component component)
    {
        flags.Remove(component);
        switch (component)
        {
        }
        EditorUtility.SetDirty(this);
    }
    
    public partial void AddComponent(Component component)
    {
        if (flags.Contains(component)) return;
        switch (component)
        {
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
            }
        }
    }
    
}
