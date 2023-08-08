using System;
using Unity.Collections;

public class ComponentArray<T> : IComponentArray where T : struct, IComponentData
{
    public T[] components;
    private bool fullArray;

    public ComponentArray(ushort size)
    {
        components = new T[size];
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
}

public interface IComponentArray
{
    public void CreateComponent(Entity entity, ushort idx);
    public void RemoveComponent(ushort idx);
    public bool CheckFullArray();
    public void SetIsFullArray(bool value);
}