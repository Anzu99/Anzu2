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
    private void GenerateArchetype()
    {
        ANZU_Generate.GenerateArchetype();
    }
    [Button(ButtonSizes.Large), GUIColor(.5f, .5f, .5f), PropertyOrder(1)]
    private void GenerateArchetypeChunk()
    {
        ANZU_Generate.GenerateArchetypeChunk();
    }
}
