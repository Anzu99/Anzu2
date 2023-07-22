using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[Serializable]
public struct MyStruct
{
    public int a;
    public int b;
}
public class Test : MonoBehaviour
{
    public string o;
    public MyStruct m;

    [Button(ButtonSizes.Large), GUIColor(0, 1, 0), PropertyOrder(100)]
    private void Add()
    {
        MyStruct myStruct = new MyStruct { a = 1, b = 2 };
        o = JsonUtility.ToJson(myStruct);

        m = JsonUtility.FromJson<MyStruct>(o);
    }
}
