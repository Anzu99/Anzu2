#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ShortKeyEngine : MonoBehaviour
{
    [MenuItem("Tools/Save Entity Prefab _s")]
    [System.Obsolete]
    static void SavePrefab()
    {
        if (PrefabStageUtility.GetCurrentPrefabStage())
        {
            GameObject[] selection = Selection.gameObjects;
            if (selection.Length < 1) return;
            foreach (GameObject go in selection)
            {
                EntityPrefab entityPrefab = go.GetComponent<EntityPrefab>();
                if (entityPrefab)
                {
                    entityPrefab.SaveData();
                    string path = PrefabStageUtility.GetCurrentPrefabStage()?.prefabAssetPath;
                    PrefabUtility.SaveAsPrefabAsset(entityPrefab.gameObject, path);
                }
            }
        }
        else
        {
            GameObject[] selection = Selection.gameObjects;
            if (selection.Length < 1) return;
            foreach (GameObject go in selection)
            {
                EntityPrefab entityPrefab = go.GetComponent<EntityPrefab>();
                if (entityPrefab)
                {
                    entityPrefab.SaveData();
                    Object o = PrefabUtility.GetPrefabParent(entityPrefab);
                    string path = AssetDatabase.GetAssetPath(o);
                    PrefabUtility.SaveAsPrefabAsset(entityPrefab.gameObject, path);
                }
            }
        }
    }
}
#endif