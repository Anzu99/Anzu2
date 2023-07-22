using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SquareCollider))]
public class SquareColliderEditor : Editor
{
    SquareCollider collider2D => (SquareCollider)target;

    void OnEnable()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;
        SceneView.duringSceneGui += this.OnSceneGUI;

        Vector2 center = collider2D.center;
        Vector2 size = collider2D.size;
        if (size == Vector2.zero)
        {
            size = collider2D.size = collider2D.transform.lossyScale;

            collider2D.dotX1 = new Vector2(-size.x / 2, 0);
            collider2D.dotX2 = new Vector2(size.x / 2, 0);
            collider2D.dotY1 = new Vector2(0, size.y / 2);
            collider2D.dotY2 = new Vector2(0, -size.y / 2);
        }
    }
    void OnDisable()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        CheckKey();
        if (collider2D.editorCollider)
        {
            float dotSize = 0.013f * sceneView.size;
            Handles.color = Color.green;
            Vector2 center = collider2D.center;
            Vector2 size = collider2D.size;
            Vector2 targetPos = collider2D.transform.position;

            collider2D.dotX1.x = Handles.FreeMoveHandle(collider2D.dotX1 + targetPos, Quaternion.identity, dotSize, Vector2.zero, Handles.DotHandleCap).x - targetPos.x;
            collider2D.dotX2.x = Handles.FreeMoveHandle(collider2D.dotX2 + targetPos, Quaternion.identity, dotSize, Vector2.zero, Handles.DotHandleCap).x - targetPos.x;
            collider2D.dotY1.y = Handles.FreeMoveHandle(collider2D.dotY1 + targetPos, Quaternion.identity, dotSize, Vector2.zero, Handles.DotHandleCap).y - targetPos.y;
            collider2D.dotY2.y = Handles.FreeMoveHandle(collider2D.dotY2 + targetPos, Quaternion.identity, dotSize, Vector2.zero, Handles.DotHandleCap).y - targetPos.y;

            if (collider2D.dotX1Cache != collider2D.dotX1 || collider2D.dotX2Cache != collider2D.dotX2)
            {
                collider2D.dotY1 = new Vector2((collider2D.dotX2.x + collider2D.dotX1.x) / 2, collider2D.dotY1.y);
                collider2D.dotY2 = new Vector2((collider2D.dotX2.x + collider2D.dotX1.x) / 2, collider2D.dotY2.y);
            }
            else if (collider2D.dotY1Cache != collider2D.dotY1 || collider2D.dotY2Cache != collider2D.dotY2)
            {
                collider2D.dotX1 = new Vector2(collider2D.dotX1.x, (collider2D.dotY1.y + collider2D.dotY2.y) / 2);
                collider2D.dotX2 = new Vector2(collider2D.dotX2.x, (collider2D.dotY1.y + collider2D.dotY2.y) / 2);
            }
            collider2D.dotX1Cache = collider2D.dotX1;
            collider2D.dotX2Cache = collider2D.dotX2;
            collider2D.dotY1Cache = collider2D.dotY1;
            collider2D.dotY2Cache = collider2D.dotY2;

            collider2D.center.x = (collider2D.dotX2.x + collider2D.dotX1.x) / 2;
            collider2D.center.y = (collider2D.dotY1.y + collider2D.dotY2.y) / 2;

            collider2D.size.x = collider2D.dotX2.x - collider2D.dotX1.x;
            collider2D.size.y = collider2D.dotY1.y - collider2D.dotY2.y;
        }
        else
        {
            collider2D.dotX1Cache = collider2D.dotX1 = (-collider2D.size.x / 2 + collider2D.center.x) * Vector2.right;
            collider2D.dotX2Cache = collider2D.dotX2 = (collider2D.size.x / 2 + collider2D.center.x) * Vector2.right;
            collider2D.dotY1Cache = collider2D.dotY1 = (collider2D.size.y / 2 + collider2D.center.y) * Vector2.up;
            collider2D.dotY2Cache = collider2D.dotY2 = (-collider2D.size.y / 2 + collider2D.center.y) * Vector2.up;
        }
    }

    void CheckKey()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.W)
            {
                collider2D.editorCollider = !collider2D.editorCollider;
            }
        }
    }
}
