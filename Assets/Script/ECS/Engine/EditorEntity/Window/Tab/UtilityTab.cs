#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

public class UtilityTab
{

}

public class UtilityDebugDrawer : OdinValueDrawer<UtilityTab>
{
    public float H = 1;
    public float S = 1;
    public float V = 1;

    protected override void DrawPropertyLayout(GUIContent label)
    {
        DrawColorRandom();
    }

    public void RandomColor()
    {
        H = UnityEngine.Random.Range(0f, 1f);
        S = UnityEngine.Random.Range(1f, 1f);
        V = UnityEngine.Random.Range(0.5f, 1f);
    }

    public void DrawColorRandom()
    {
        Color _color = Color.HSVToRGB(H, S, V);
        GUIHelper.PushColor(_color);
        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        GUIHelper.PopColor();
        GUILayout.Label("Title", SirenixGUIStyles.LabelCentered);
        SirenixEditorGUI.EndBoxHeader();
        SirenixEditorFields.TextField("H", H.ToString());
        SirenixEditorFields.TextField("S", S.ToString());
        SirenixEditorFields.TextField("V", V.ToString());
        GUILayout.Space(10);


        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Random Color"))
        {
            RandomColor();
        }
        GUILayout.EndHorizontal();

        SirenixEditorGUI.EndBox();

    }

    private Texture2D MakeBackgroundTexture(int width, int height, Color color)
    {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }

        Texture2D backgroundTexture = new Texture2D(width, height);

        backgroundTexture.SetPixels(pixels);
        backgroundTexture.Apply();

        return backgroundTexture;
    }
}
#endif