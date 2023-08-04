using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

public class Archetype
{
    public Flag flag;
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
        CreateNewChunk();
    }


    public void AddEntity(Entity entity)
    {
        if (!listArchetypeChunks[^1].AddEntity(entity))
        {
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

    public ComponentEditor<T> GetComponentEditor<T>(Component component, ushort idEntity) where T : struct, IComponentData
    {
        foreach (var archetype in listArchetypeChunks)
        {
            if (archetype.ContainEntity(idEntity))
            {
                return archetype.GetComponentEditor<T>(idEntity, GetIndiceComponent(component));
            }
        }
        return null;
    }

    public Entity GetEntity(ushort idEntity)
    {
        foreach (var archetype in listArchetypeChunks)
        {
            if (archetype.ContainEntity(idEntity))
            {
                return archetype.GetEntity(idEntity);
            }
        }
        return null;
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

    private ushort GetIndiceComponent(Component component)
    {
        for (ushort i = 0; i < components.Length; i++)
        {
            if (component == components[i])
            {
                return i;
            }
        }
        return 0;
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
    private List<IComponentArray> componentArrays;
    private ushort count;
    private ushort chunkSize;

    public bool isEmpty => count <= 0;

    public ArchetypeChunk(Component[] components, ushort chunkSize)
    {
        count = 1;
        this.chunkSize = chunkSize;
        entityIndices = new Dictionary<ushort, ushort>();
        componentArrays = new List<IComponentArray>();

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
            for (var i = 0; i < componentArrays.Count; i++)
            {
                componentArrays[i].CreateComponent(entity, (ushort)(count - 1));
                entityIndices.Add(entity.idEntity, (ushort)(count - 1));
                count++;
                componentArrays[i].SetIsFullArray(count >= chunkSize);
            }
            return true;
        }
    }

    public ComponentEditor<T> GetComponentEditor<T>(ushort idEntity, ushort componentIndice) where T : struct, IComponentData
    {
        ComponentArray<T> componentArray = (ComponentArray<T>)componentArrays[componentIndice];
        return componentArray.GetComponentEditor(entityIndices[idEntity]);
    }

    public Entity GetEntity(ushort idEntity)
    {
        return componentArrays[0].GetEntity(entityIndices[idEntity]);
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

    public bool ContainEntity(ushort entityID)
    {
        return entityIndices.ContainsKey(entityID);
    }

    private void CreateComponentArray(Component component, ushort size, int index)
    {
        switch (component)
        {
            case Component.TransformComponent:
                componentArrays[index] = new ComponentArray<TransformComponent>(size);
                break;
        }
    }

}