using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

public class Archetype
{
    private Flag flag;
    private List<ArchetypeChunk> listArchetypeChunks;
    private ushort chunkSize;
    private Component[] components;
    private Dictionary<Component, byte> componentIndices;
    public bool isEmpty => listArchetypeChunks.Count <= 0;
    public Archetype(Flag flag)
    {
        componentIndices = new Dictionary<Component, byte>();
        listArchetypeChunks = new List<ArchetypeChunk>();
        this.flag = flag;
        this.components = flag.ToComponents();
    }


    public void AddEntity(Entity entity)
    {
        if (!listArchetypeChunks[^1].AddEntity(entity))
        {
            CreateNewChunk();
            AddEntity(entity);
        }
    }

    public void CreateNewChunk()
    {
        ArchetypeChunk archetypeChunk = new ArchetypeChunk(components, chunkSize);
        listArchetypeChunks.Add(archetypeChunk);
    }

    public IEnumerable<ComponentArray<T>> GetComponentArrayData<T>(Component component) where T : struct, IComponentData
    {
        foreach (var item in listArchetypeChunks)
        {
            yield return item.GetComponentArray<T>(componentIndices[component]);
        }
    }

    public void RemoveEntity(ushort idEntity)
    {
        foreach (var item in listArchetypeChunks)
        {
            if (item.RemoveEntity(idEntity))
            {
                if (item.isEmpty)
                {
                    item.DestroyChunk();
                    listArchetypeChunks.Remove(item);
                }
                break;
            }
        }
    }

    public bool ContainFlag(Flag otherFlag)
    {
        return flag.ContainFlag(otherFlag);
    }
    public bool CompareArchetype(Flag otherFlag)
    {
        return flag.Equal(otherFlag);
    }
}


public class ArchetypeChunk
{
    private Dictionary<ushort, ushort> entityIndices;
    private IcomponentArray[] componentArrays;
    private ushort count;
    private ushort chunkSize;

    public bool isEmpty => count <= 0;

    public ArchetypeChunk(Component[] components, ushort chunkSize)
    {
        count = 1;
        this.chunkSize = chunkSize;
        entityIndices = new Dictionary<ushort, ushort>();
        componentArrays = new IcomponentArray[components.Length];
        for (var i = 0; i < components.Length; i++)
        {
            CreateComponentArray(components[i], chunkSize, i);
        }
    }
    public ComponentArray<T> GetComponentArray<T>(byte componentidx) where T : struct, IComponentData
    {
        return (ComponentArray<T>)componentArrays[componentidx];
    }

    public bool AddEntity(Entity entity)
    {
        if (count >= chunkSize)
        {
            return false;
        }
        else
        {
            for (var i = 0; i < componentArrays.Length; i++)
            {
                componentArrays[i].CreateComponent(entity, (ushort)(count - 1));
                entityIndices.Add(entity.idEntity, (ushort)(count - 1));
                count++;
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

    public void DestroyChunk()
    {
        foreach (var item in componentArrays)
        {
            item.OnDestroy();
        }
    }

    private bool ContainEntity(ushort entityID)
    {
        return entityIndices.ContainsKey(entityID);
    }

    private void CreateComponentArray(Component component, ushort size, int index)
    {
        switch (component)
        {
            case Component.Transform:
                componentArrays[index] = new ComponentArray<TransformComponent>(size);
                break;
        }
    }

}