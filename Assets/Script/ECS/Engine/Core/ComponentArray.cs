using System;
using Unity.Collections;

public class ComponentArray<T> : IComponentArray where T : struct, IComponentData
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


    public Entity GetEntity(ushort indice)
    {
        return components[indice].GetEntity();
    }

    public ComponentEditor<T> GetComponentEditor(ushort componentIndice)
    {
        ComponentEditor<T> componentEditor = new ComponentEditor<T>()
        {
            array = components,
            index = componentIndice
        };
        return componentEditor;
    }

}

public interface IComponentArray
{
    public Entity GetEntity(ushort indice);
    public void CreateComponent(Entity entity, ushort idx);
    public void RemoveComponent(ushort idx);
    bool CheckFullArray();
    void SetIsFullArray(bool value);
    public void OnDestroy();
}