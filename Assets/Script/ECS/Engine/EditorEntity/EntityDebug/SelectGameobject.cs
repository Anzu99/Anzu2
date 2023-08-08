using UnityEngine;
using UnityEditor;
public class SelectGameobject : MonoBehaviour
{
    private void Awake()
    {
        ShowFPS.ShowFPSHandle();
    }
    GameObject selectedObject;
    private void Update()
    {
        selectedObject = Selection.activeGameObject;
        if (selectedObject != null)
        {
            EntityEditor entityEditor = selectedObject.GetComponent<EntityEditor>();
            if (entityEditor != null)
            {
                entityEditor?.OnUpdate();
            }
        }
    }
}

