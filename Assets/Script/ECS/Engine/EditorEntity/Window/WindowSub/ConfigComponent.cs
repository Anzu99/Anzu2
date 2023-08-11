#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Examples;
using UnityEditor;
using UnityEngine;

public class ConfigComponentWindow : OdinEditorWindow
{
    [MenuItem("ANZU/Editor Manager/Config Component")]
    private static void OpenWindow()
    {
        GetWindow<ConfigComponentWindow>().Show();
    }
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(0)]
    public void Save()
    {
        componentEditorData.Save();
        componentTab.Fix();
        Close();
    }

    private ComponentEditorData componentEditorData;
    private List<ComponentConfigItem> listComponentConfigsItemBeforeUpdate;
    [Searchable, HideLabel, OnValueChanged("OnChangeConfig")]
    public List<ComponentConfigItem> listComponentConfigItems;

    ComponentTab componentTab;
    public void Initialize(ComponentTab componentTab)
    {
        this.componentTab = componentTab;
    }
    protected override void OnEnable()
    {
        componentEditorData = new ComponentEditorData();
        componentEditorData.LoadData();
        listComponentConfigItems = componentEditorData.componentConfigData.componentConfigItems;
        listComponentConfigsItemBeforeUpdate = new List<ComponentConfigItem>(listComponentConfigItems);
        base.OnEnable();
    }

    public void Update()
    {
        listComponentConfigItems.Sort((student1, student2) => student1.order.CompareTo(student2.order));
    }

    public void OnChangeConfig()
    {
        for (var i = 0; i < listComponentConfigItems.Count; i++)
        {
            if (listComponentConfigItems[i].component != listComponentConfigsItemBeforeUpdate[i].component)
            {
                listComponentConfigItems[i].order = i;
            }
        }
        listComponentConfigsItemBeforeUpdate = new List<ComponentConfigItem>(listComponentConfigItems);
    }

}
#endif