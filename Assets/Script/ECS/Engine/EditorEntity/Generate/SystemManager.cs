public partial class SystemManager
{
    public partial void CreateSystem(params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            SystemBase systemBase = null;
            switch (item)
            {
                case ESystem.InputSystem:
                    systemBase = new InputSystem();
                    break;
                case ESystem.MoveSystem:
                    systemBase = new MoveSystem();
                    break;
            }
            systemBase.Start();
            listSystem.Add(systemBase);
            systemIndice.Add(item, count++);
        }
    }
}

public enum ESystem
{
    InputSystem,
    MoveSystem,
}
