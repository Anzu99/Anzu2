using System;
using Unity.Collections;

public class ComponentArray<T> : IcomponentArray where T : struct, IComponentData
{
    public NativeArray<T> components;
    public ushort count;

    public ComponentArray(ushort size)
    {
        count = 1;
        components = new NativeArray<T>(size, Allocator.Temp);
    }

    public void CreateComponent(Entity entity)
    {
        components[count - 1].SetEntity(entity);
        components[count - 1].Start();
        count++;
    }

    public void RemoveComponent(ushort idEntity)
    {

    }
}

public interface IcomponentArray
{
    public void CreateComponent(Entity entity);
}