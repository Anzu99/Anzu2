using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorForceRepaint : MonoBehaviour
{
    public float val = 0;
    private void Update()
    {
        val += Time.deltaTime;
    }
    private void OnDisable()
    {
        val = 0;
    }
}
