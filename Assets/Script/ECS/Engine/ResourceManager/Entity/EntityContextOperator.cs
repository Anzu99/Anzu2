#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class EntityContextOperator : OdinEditorWindow
{
    [PropertySpace, Button("Remove Component", ButtonSizes.Large), GUIColor(1, 0.2f, 0)]
    private void RemoveComponent()
    {
        entityPrefab.RemoveComponent(component);
        Close();
    }

    [PropertySpace, Button("Copy Component", ButtonSizes.Large), GUIColor(0, 1, 0)]
    private void CopyComponent()
    {
        entityPrefab.CopyComponent(component);
        Close();
    }

    [PropertySpace, Button("Paste Component", ButtonSizes.Large), GUIColor(1, 1, 0)]
    private void PasteComponentComponent()
    {
        entityPrefab.PasteComponent(component);
        Close();
    }


    private EntityPrefab entityPrefab;
    private Component component;
    public void SetUp(Component component, EntityPrefab entityPrefab)
    {
        this.component = component;
        this.entityPrefab = entityPrefab;
    }

}
#endif