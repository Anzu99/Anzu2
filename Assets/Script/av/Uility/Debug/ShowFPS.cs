using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    private float xShow = 0;
    private float yShow = 0;
    private float deltaTime;

    public void SetShowFPS(float xShow, float yShow)
    {
        this.xShow = xShow;
        this.yShow = yShow;
    }

    public static void ShowFPSHandle(float x = 0, float y = .5f)
    {
        GameObject go = new GameObject("FPS");
        go.AddComponent<ShowFPS>().SetShowFPS(x, y);
        DontDestroyOnLoad(go);
    }


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        var msec = deltaTime * 1000.0f;
        var fps = 1.0f / deltaTime;
        var text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        int w = Screen.width, h = Screen.height;
        var style = new GUIStyle();
        var rect = new Rect(xShow * w, yShow * h, w, 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 5 / 110;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.green;
        GUI.Label(rect, text, style);
    }
}
