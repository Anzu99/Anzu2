#if UNITY_EDITOR
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using System;
public class EditorManager : OdinMenuEditorWindow
{
    public static List<Action> updateActions = new List<Action>();

    [MenuItem("ANZU/Editor Manager")]
    private static void OpenWindow()
    {
        GetWindow<EditorManager>().Show();
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;
        tree.UpdateMenuTree();
        tree.Add("General", new GeneralTab());
        tree.Add("Component", new ComponentTab());
        tree.Add("System", new SystemTab());
        tree.Add("Utility", new UtilityTab());
        return tree;
    }

    override protected void OnGUI()
    {
        foreach (var item in updateActions)
        {
            item?.Invoke();
        }
        base.OnGUI();
    }

    override protected void OnDestroy()
    {
        updateActions.Clear();
        base.OnDestroy();
    }

}
#endif