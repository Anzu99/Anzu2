
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

public class ArchetypeChunk
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
            CreateComponentArray(components[i], chunkSize, i);
        }
    }
    public ComponentArray<T> GetComponentArray<T>(ushort componentidx) where T : struct, IComponentData
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

    private void CreateComponentArray(Component component, ushort size, int index)
    {
        switch (component)
        {
            case Component.TransformComponent:
                ComponentArray<TransformComponent> TransformComponents = new ComponentArray<TransformComponent>(size);
                componentArrays.Add(TransformComponents);
                break;
            case Component.MoveComponent:
                ComponentArray<MoveComponent> MoveComponents = new ComponentArray<MoveComponent>(size);
                componentArrays.Add(MoveComponents);
                break;
        }
    }

}