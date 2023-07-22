using System;
#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

public class ColoredFoldoutGroupAttributeDrawer : OdinGroupDrawer<ColoredFoldoutGroupAttribute>
{
    private LocalPersistentContext<bool> isExpanded;
    protected override void Initialize()
    {
        this.isExpanded = this.GetPersistentValue<bool>(
            "ColoredFoldoutGroupAttributeDrawer.isExpanded",
            GeneralDrawerConfig.Instance.ExpandFoldoutByDefault);
    }

    Rect rect;
    protected override void DrawPropertyLayout(GUIContent label)
    {
        GUIHelper.PushColor(new Color(this.Attribute.R, this.Attribute.G, this.Attribute.B, this.Attribute.A));
        rect = SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();
        CheckContextClick();
        CheckClickOutsideWindowContex();
        GUIHelper.PopColor();
        this.isExpanded.Value = SirenixEditorGUI.Foldout(this.isExpanded.Value, label);
        SirenixEditorGUI.EndBoxHeader();

        if (SirenixEditorGUI.BeginFadeGroup(this, this.isExpanded.Value))
        {
            for (int i = 0; i < this.Property.Children.Count; i++)
            {
                this.Property.Children[i].Draw();
            }
        }

        SirenixEditorGUI.EndFadeGroup();
        SirenixEditorGUI.EndBox();
    }
    EntityContextOperator entityContextOperator;
    Rect entityContextOperatorRect;
    public void CheckContextClick()
    {
        if (Event.current.type == EventType.ContextClick)
        {
            Vector2 mousePos = Event.current.mousePosition;
            if (rect.Contains(mousePos))
            {
                mousePos = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                entityContextOperator = EditorWindow.GetWindow<EntityContextOperator>();

                EntityPrefab entityPrefab = Property.ParentValueProperty.ParentValues[0] as EntityPrefab;
                entityContextOperator.SetUp(Attribute.component, entityPrefab);
                entityContextOperator.Show();
                Rect r = new Rect(mousePos, new Vector2(200, 500));
                entityContextOperatorRect = r;
                entityContextOperator.position = r;
            }
        }
    }

    public void CheckClickOutsideWindowContex()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            Vector2 mousePos = Event.current.mousePosition;
            if (!entityContextOperatorRect.Contains(mousePos))
            {
                entityContextOperator?.Close();
            }
        }
    }


}
public class ColoredFoldoutGroupAttribute : PropertyGroupAttribute
{
    public float R, G, B, A;
    public Component component;
    public ColoredFoldoutGroupAttribute(Component component)
        : base(component.ToString())
    {
    }

    public ColoredFoldoutGroupAttribute(Component component, float r, float g, float b, float a = 1f)
        : base(component.ToString())
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
        this.component = component;
    }

    protected override void CombineValuesWith(PropertyGroupAttribute other)
    {
        var otherAttr = (ColoredFoldoutGroupAttribute)other;

        this.R = Math.Max(otherAttr.R, this.R);
        this.G = Math.Max(otherAttr.G, this.G);
        this.B = Math.Max(otherAttr.B, this.B);
        this.A = Math.Max(otherAttr.A, this.A);
    }
}
#endif