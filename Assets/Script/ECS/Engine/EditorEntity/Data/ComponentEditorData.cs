#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using AV;
using System.IO;
using UnityEditor;

[Serializable]
public class ComponentEditorData
{
    public ComponentConfigData componentConfigData;

    public void LoadData()
    {
        string jsonData = LoadJsonAsString("EditorData/EditorData");
        if (string.IsNullOrEmpty(jsonData))
        {
            CreateComponentConfigData();
            SortOrder();
        }
        else
        {
            var val = JsonUtility.FromJson<ComponentConfigData>(jsonData);
            if (val == null) UnityEngine.Debug.LogError("EditorData illegal");
            else componentConfigData = val;
        }
    }

    public void CreateComponentConfigData()
    {
        int defaultOrder = 1;
        componentConfigData = new ComponentConfigData();

        Component componentTmp = Component.None;
        for (var i = 1; i < Enum.GetNames(typeof(Component)).Length; i++)
        {
            componentTmp = AV_ENUM_EXTENSION.Next(componentTmp);
            ComponentConfigItem componentConfigItem = new ComponentConfigItem()
            {
                component = componentTmp,
                order = defaultOrder,
            };
            componentConfigData.componentConfigItems.Add(componentConfigItem);
        }
        Save();
    }

    public void FixComponentConfigData()
    {
        int defaultOrder = componentConfigData.componentConfigItems[^1].order;

        Component componentTmp = Component.None;
        for (var i = 1; i < Enum.GetNames(typeof(Component)).Length; i++)
        {
            componentTmp = AV_ENUM_EXTENSION.Next(componentTmp);

            if (!CheckExistComponentConfig(componentTmp))
            {
                ComponentConfigItem componentConfigItem = new ComponentConfigItem()
                {
                    component = componentTmp,
                    order = defaultOrder++,
                };
                componentConfigData.componentConfigItems.Add(componentConfigItem);
            }
        }

        bool CheckExistComponentConfig(Component component)
        {
            foreach (var item in componentConfigData.componentConfigItems)
            {
                if (item.component == component) return true;
            }
            return false;
        }
        CheckRemoveComponent();
        Save();
    }

    public void CheckRemoveComponent()
    {
        string filePath = $"Assets/Script/ECS/Component";
        string[] componentNames = DirectoryUtility.GetAllFileInFolderAndChild(filePath, "cs");
        List<string> listComponentNames = new List<string>(componentNames);

        for (var i = 0; i < componentConfigData.componentConfigItems.Count; i++)
        {
            if (!listComponentNames.Contains(componentConfigData.componentConfigItems[i].component.ToString()))
            {
                componentConfigData.componentConfigItems.Remove(componentConfigData.componentConfigItems[i--]);
            }
        }

    }

    private void SortOrder()
    {
        for (var i = 0; i < componentConfigData.componentConfigItems.Count; i++)
        {
            componentConfigData.componentConfigItems[i].order = i;
        }
        Save();
    }

    public void Save()
    {
        string filePath = "Assets/Resources/EditorData";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            AssetDatabase.Refresh();
        }
        string jsonData = JsonUtility.ToJson(componentConfigData);
        filePath += "/EditorData.json";

        File.WriteAllText(filePath, jsonData);
        AssetDatabase.Refresh();
    }

    private string LoadJsonAsString(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null)
        {
            return textAsset.text;
        }
        else
        {
            Debug.Log("Create: " + path);
            return string.Empty;
        }
    }
}

[Serializable]
public class ComponentConfigData
{
    public List<ComponentConfigItem> componentConfigItems;

    public ComponentConfigData()
    {
        componentConfigItems = new List<ComponentConfigItem>();
    }

}
[Serializable]
public class ComponentConfigItem
{
    [ReadOnly] public Component component;
    public int order;
    public bool foldout = true;
}
#endif