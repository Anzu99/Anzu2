using System.Collections.Generic;
public partial class SystemManager
{
    List<SystemBase> listSystem;
    Dictionary<ESystem, ushort> systemIndice;
    ushort count = 0;
    public SystemManager()
    {
        listSystem = new List<SystemBase>();
        systemIndice = new Dictionary<ESystem, ushort>();
    }

    public partial void CreateSystem(params ESystem[] systems);

    public void RemoveSystem(params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            listSystem.RemoveAt(systemIndice[item]);
            systemIndice.Remove(item);
        }
    }

    public void ActiveSystem(bool active, params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            listSystem[systemIndice[item]].active = active;
        }
    }

    public void UpdateSystem()
    {
        foreach (var system in listSystem)
        {
            system.Update();
        }
    }

}