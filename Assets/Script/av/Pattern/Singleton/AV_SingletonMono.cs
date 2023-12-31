
using UnityEngine;
public class AV_SingletonMono<T> : MonoBehaviour where T : UnityEngine.Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            // DontDestroyOnLoad(this.gameObject);
        }
    }
}
