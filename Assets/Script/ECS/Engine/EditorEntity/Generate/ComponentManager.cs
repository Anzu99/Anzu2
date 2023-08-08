
public partial class ComponentManager
{
    public void CreateComponentArray(Component flag)
    {
        ushort _maxCapacity = GetCapacity(flag);
        switch (flag)
        {
        }
    }
        
    public void RemoveComponent(Component component, ushort idEntity)
    {
        switch (component)
        {
        }
    }
        
    public ushort GetCapacity(Component flag)
    {
        return flag switch
        {  
            _ => 32
        };
    }
        
    public void CreateComponent(Component flag, ushort idEntity)
    {
        switch (flag)
        {
        }
    }
        
    public void InitializeComponent(Component component, ushort idEntity)
    {
        switch (component)
        {
        }
    }
}

[System.Serializable]
public enum Component
{
    None,
    TransformComponent,
    MoveComponent
}
        
