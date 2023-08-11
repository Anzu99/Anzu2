
public partial class Archetype
{
    public void CopyComponentData(EntityDataCache entityDataCache, Entity entity)
    {
        foreach (var data in entityDataCache.dictDataCopy)
        {
            if (componentIndices.ContainsKey(data.Key))
            {
                Handle(data.Key, data.Value);
            }
        }
        void Handle(Component component, object data)
        {
            switch (component)
            {
                case Component.InputComponent:
                    GetComponent<InputComponent>(component, entity.idEntity) = (InputComponent)data;
                    break;
                case Component.MoveComponent:
                    GetComponent<MoveComponent>(component, entity.idEntity) = (MoveComponent)data;
                    break;
                case Component.TransformComponent:
                    GetComponent<TransformComponent>(component, entity.idEntity) = (TransformComponent)data;
                    break;
            }
        }

    }

}

public enum Component
{
    None,
    InputComponent,
    MoveComponent,
    TransformComponent,

}
