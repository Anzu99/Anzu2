using System;
using Unity.Collections;

public class ComponentArray<T> : IcomponentArray where T : struct, IComponentData
{
    private NativeArray<T> components;
    private bool fullArray;

    public ComponentArray(ushort size)
    {
        components = new NativeArray<T>(size, Allocator.Temp);
    }

    public void CreateComponent(Entity entity, ushort idx)
    {
        components[idx].SetEntity(entity);
        components[idx].Start();
    }
    public void RemoveComponent(ushort idx)
    {
        components[idx].SetEntity(null);
    }

    public bool CheckFullArray()
    {
        return fullArray;
    }

    public void SetIsFullArray(bool value)
    {
        fullArray = value;
    }

    public void OnDestroy()
    {
        components.Dispose();
    }
}

public interface IcomponentArray
{
    public void CreateComponent(Entity entity, ushort idx);
    public void RemoveComponent(ushort idx);
    bool CheckFullArray();
    void SetIsFullArray(bool value);
    public void OnDestroy();
}