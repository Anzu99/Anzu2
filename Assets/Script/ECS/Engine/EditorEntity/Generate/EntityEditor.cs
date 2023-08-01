
using Sirenix.OdinInspector;
using UnityEngine;
public partial class EntityEditor : MonoBehaviour
{
    public partial void SetUpDataPrefab(EntityPrefab entityPrefab)
    {
        for (var i = 0; i < entityPrefab.flags.Count; i++)
        {
            SetData(i);
        }
        Destroy(entityPrefab);
        void SetData(int index)
        {
            switch (entityPrefab.flags[index])
            {
                
            }
        }
    }
        
}
