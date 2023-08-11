
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public partial class ArchetypeChunk
{
    public ushort count;
    private Dictionary<ushort, ushort> entityIndices;
    [ShowInInspector]
    private List<IComponentArray> componentArrays;
    private ushort chunkSize;

    public bool isEmpty => count <= 0;

    public ArchetypeChunk(Component[] components, ushort chunkSize)
    {
        count = 0;
        this.chunkSize = chunkSize;
        entityIndices = new Dictionary<ushort, ushort>();
        componentArrays = new List<IComponentArray>();

        for (var i = 0; i < components.Length; i++)
        {
            CreateComponentArray(components[i], chunkSize);
        }
    }
    public ComponentArray<T> GetComponentArray<T>(byte componentidx) where T : struct, IComponentData
    {
        return (ComponentArray<T>)componentArrays[componentidx];
    }

    public ref T GetComponent<T>(byte componentidx, ushort idEntity) where T : struct, IComponentData
    {
        ComponentArray<T> componentArray = GetComponentArray<T>(componentidx);
        return ref componentArray.components[entityIndices[idEntity]];
    }


    public bool AddEntity(Entity entity)
    {
        if (count >= chunkSize)
        {
            return false;
        }
        else
        {
            count++;
            entityIndices.Add(entity.idEntity, (ushort)(count - 1));
            for (var i = 0; i < componentArrays.Count; i++)
            {
                componentArrays[i].CreateComponent(entity, (ushort)(count - 1));
                componentArrays[i].SetIsFullArray(count >= chunkSize);
            }
            return true;
        }
    }

    public bool RemoveEntity(ushort entityID)
    {
        if (ContainEntity(entityID))
        {
            foreach (var item in componentArrays)
            {
                item.RemoveComponent(entityIndices[entityID]);
                item.SetIsFullArray(false);
            }
            count--;
            entityIndices.Remove(entityID);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ContainEntity(ushort entityID)
    {
        return entityIndices.ContainsKey(entityID);
    }
}

public struct EntityDataCache
{
    public Dictionary<Component, object> dictDataCopy;

    public void AddData(Component component, object data)
    {
        if(dictDataCopy == null)dictDataCopy = new Dictionary<Component, object>();
        dictDataCopy.Add(component, data);
    }
}