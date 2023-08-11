using System.Collections.Generic;
public partial class SystemManager
{
    List<SystemBase> listSystem;
    Dictionary<ESystem, ushort> systemIndices;
    ushort count = 0;
    public SystemManager()
    {
        listSystem = new List<SystemBase>();
        systemIndices = new Dictionary<ESystem, ushort>();
    }

    public partial void CreateSystem(params ESystem[] systems);

    public void RemoveSystem(params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            listSystem.RemoveAt(systemIndices[item]);
            systemIndices.Remove(item);
        }
    }

    public void ActiveSystem(bool active, params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            listSystem[systemIndices[item]].active = active;
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