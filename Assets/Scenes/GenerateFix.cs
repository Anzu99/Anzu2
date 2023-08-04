using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class GenerateFix : MonoBehaviour
{
    [PropertySpace(2), Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f)]
    public void GenerateEntityEditorFix()
    {
        ANZU_Generate.GenerateEntityEditor();
    }
}
