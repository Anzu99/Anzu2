#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
using System;
public class LockInspector
{
    [MenuItem("Tools/Toggle Inspector Lock %q")]
    static void ToggleInspectorLock()
    {
        EditorWindow inspectorToBeLocked = EditorWindow.mouseOverWindow; 

        if (inspectorToBeLocked != null && inspectorToBeLocked.GetType().Name == "InspectorWindow")
        {
            Type type = Assembly.GetAssembly(typeof(UnityEditor.Editor)).GetType("UnityEditor.InspectorWindow");
            PropertyInfo propertyInfo = type.GetProperty("isLocked");
            bool value = (bool)propertyInfo.GetValue(inspectorToBeLocked, null);
            propertyInfo.SetValue(inspectorToBeLocked, !value, null);

            inspectorToBeLocked.Repaint();
        }
    }
}
#endif
