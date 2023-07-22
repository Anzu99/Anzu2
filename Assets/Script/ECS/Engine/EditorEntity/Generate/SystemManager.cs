
public partial class SystemManager
{
    public partial void CreateSystem(params ESystem[] systems)
    {
        foreach (var item in systems)
        {
            SystemBase systemBase = null;
            switch (item)
            {
            }
            systemBase.Start();
            listSystem.Add(systemBase);
            systemIndice.Add(item, count++);
        }
    }
}

public enum ESystem
{
}
