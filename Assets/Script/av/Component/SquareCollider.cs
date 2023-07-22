using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareCollider : MonoBehaviour
{
#if UNITY_EDITOR
    public bool editorCollider = false;
    public Color color = Color.green;

    [HideInInspector] public Vector2 dotX1, dotX2, dotY1, dotY2;
    [HideInInspector] public Vector2 dotX1Cache, dotX2Cache, dotY1Cache, dotY2Cache;
#endif
    public Vector2 size = Vector2.zero;
    public Vector2 center = Vector2.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(center + (Vector2)transform.position, size);
    }
}
