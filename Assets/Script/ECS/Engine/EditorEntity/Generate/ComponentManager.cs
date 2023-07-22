
public partial class ComponentManager
{
    public void CreateComponentArray(Component flag)
    {
        ushort _maxCapacity = GetCapacity(flag);
        switch (flag)
        {
            case Component.InfoComponent:
                CreateComponentArray<InfoComponent>(_maxCapacity, flag);
                break;
            case Component.MovementComponent:
                CreateComponentArray<MovementComponent>(_maxCapacity, flag);
                break;
        }
    }
        
    public void RemoveComponent(Component component, ushort idEntity)
    {
        switch (component)
        {
            case Component.InfoComponent:
                RemoveComponent<InfoComponent>(component, idEntity);
                break;
            case Component.MovementComponent:
                RemoveComponent<MovementComponent>(component, idEntity);
                break;
        }
    }
        
    public ushort GetCapacity(Component flag)
    {
        return flag switch
        {  
            Component.InfoComponent => 32,
            Component.MovementComponent => 32,
            _ => 32
        };
    }
        
    public void CreateComponent(Component flag, ushort idEntity)
    {
        switch (flag)
        {
            case Component.InfoComponent:
                ComponentArray<InfoComponent> InfoComponents = dicsComponent[flag] as ComponentArray<InfoComponent>;
                InfoComponents.CreateComponent(idEntity);
                break;
            case Component.MovementComponent:
                ComponentArray<MovementComponent> MovementComponents = dicsComponent[flag] as ComponentArray<MovementComponent>;
                MovementComponents.CreateComponent(idEntity);
                break;
        }
    }
        
    public void InitializeComponent(Component component, ushort idEntity)
    {
        switch (component)
        {
            case Component.InfoComponent:
                ComponentArray<InfoComponent> InfoComponents = dicsComponent[component] as ComponentArray<InfoComponent>;
                InfoComponents.InitializeComponent(idEntity);
                break;
            case Component.MovementComponent:
                ComponentArray<MovementComponent> MovementComponents = dicsComponent[component] as ComponentArray<MovementComponent>;
                MovementComponents.InitializeComponent(idEntity);
                break;
        }
    }
}

[System.Serializable]
public enum Component
{
    None,
    InfoComponent,
    MovementComponent,
}
        
