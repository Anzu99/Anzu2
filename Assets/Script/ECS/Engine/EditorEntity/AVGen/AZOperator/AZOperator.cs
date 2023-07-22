using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class AZOperator : MonoBehaviour
{
    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    private void GenerateEntityEditor()
    {
        ANZU_Generate.GenerateEntityEditor();
    }
    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    private void GenerateEntityPrefab()
    {
        ANZU_Generate.GenerateEntityPrefab();
    }

    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    private void GenerateComponentManager()
    {
        ANZU_Generate.GenerateComponentManager();
    }
}
