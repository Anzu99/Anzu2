#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(AutoComponentAttribute))]
public class GetComponentPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        AutoComponentAttribute autoComponentAttribute = (AutoComponentAttribute)attribute;
        Type type = autoComponentAttribute.type;
        UnityEngine.Component component = property.serializedObject.targetObject as UnityEngine.Component;
        GameObject gameObject = component.gameObject;

        if (property.objectReferenceValue == null)
        {
            if (gameObject.GetComponent(type) != null)
            {
                property.objectReferenceValue = gameObject.GetComponent(type);
            }
            else
            {
                property.objectReferenceValue = gameObject.AddComponent(type);
            }
        }

        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;

    }
}
#endif

