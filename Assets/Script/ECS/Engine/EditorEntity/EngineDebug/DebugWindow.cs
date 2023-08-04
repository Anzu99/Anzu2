#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using System.Reflection;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using System.Collections.Generic;
using System;
using Sirenix.OdinInspector.Demos;
using AV;

public class DebugWindow : OdinMenuEditorWindow
{
    [MenuItem("ANZU/Engine Debug")]
    private static void OpenWindow()
    {
        GetWindow<DebugWindow>().Show();
    }

    private void Update()
    {
        // EngineDebug engineDebug = (EngineDebug)MenuTree.GetMenuItem("Utilities").Value;
        // engineDebug.OnUpdate();
    }


    protected override OdinMenuTree BuildMenuTree()
    {
        OdinMenuTree tree = new OdinMenuTree();
        tree.Selection.SupportsMultiSelect = false;
        tree.UpdateMenuTree();
        // tree.Add("Archetype", new ArchetypeDebug());
        tree.Add("Utility", new UtilityTab());
        tree.AddAllAssetsAtPath("Odin Settings", "Assets/Plugins/Sirenix", typeof(ScriptableObject), true, true);
        return tree;
    }
}
#endif