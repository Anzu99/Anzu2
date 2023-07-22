using UnityEngine;
using UnityEditor;
public class SelectGameobject : MonoBehaviour
{
    public static int SelectedEntityID = -1;
    GameObject selectedObject;

    private void Update()
    {
        selectedObject = Selection.activeGameObject;
        if (selectedObject != null)
        {
            EntityEditor entityEditor = selectedObject.GetComponent<EntityEditor>();
            if (entityEditor != null)
            {
                SelectedEntityID = entityEditor.IdEntity;
                entityEditor?.OnUpdate();
            }
        }
        else
        {
            SelectedEntityID = -1;
        }
    }
}

