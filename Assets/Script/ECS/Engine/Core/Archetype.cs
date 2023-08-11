using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Jobs;

public partial class Archetype
{
    public Flag flag;
    [ShowInInspector] public List<ArchetypeChunk> listArchetypeChunks;
    private readonly ushort chunkSize = 4;
    private readonly Component[] components;
    private readonly Dictionary<Component, byte> componentIndices;
    public bool IsEmpty => listArchetypeChunks.Count <= 0;
    public Archetype(Flag flag, ushort chunkSize)
    {
        this.chunkSize = chunkSize == 0 ? ConfigCapacity.defaultChunkSize : chunkSize;
        componentIndices = new Dictionary<Component, byte>();
        listArchetypeChunks = new List<ArchetypeChunk>();
        this.flag = flag;
        this.components = flag.ToComponents();
        for (byte i = 0; i < components.Length; i++)
        {
            componentIndices.Add(components[i], i);
        }
        CreateNewChunk();
    }


    public void AddEntity(Entity entity)
    {
        if (listArchetypeChunks.Count == 0) CreateNewChunk();
        if (!listArchetypeChunks[^1].AddEntity(entity))
        {
            CreateNewChunk();
            AddEntity(entity);
        }
        else
        {
            entity.archetype = this;
        }
    }

    public void AddEntity(Entity entity, EntityDataCache entityDataCache)
    {
        if (listArchetypeChunks.Count == 0) CreateNewChunk();
        if (!listArchetypeChunks[^1].AddEntity(entity))
        {
            CreateNewChunk();
            AddEntity(entity, entityDataCache);
        }
        else
        {
            entity.archetype = this;
            CopyComponentData(entityDataCache, entity);
        }
    }

    public void CreateNewChunk()
    {
        ArchetypeChunk archetypeChunk = new ArchetypeChunk(components, chunkSize);
        listArchetypeChunks.Add(archetypeChunk);
    }


    public ref T GetComponent<T>(Component component, ushort idEntity) where T : struct, IComponentData
    {
        foreach (var archetype in listArchetypeChunks)
        {
            if (archetype.ContainEntity(idEntity))
            {
                return ref archetype.GetComponent<T>(componentIndices[component], idEntity);
            }
        }
        //TODO code ban
        return ref listArchetypeChunks[0].GetComponent<T>(componentIndices[component], idEntity);
    }

    public EntityDataCache GetEntityDataCopy(Entity entity)
    {
        ArchetypeChunk archetypeChunk = GetArchetypeChunk(entity);
        EntityDataCache entityDataCache = new EntityDataCache();
        foreach (var component in componentIndices)
        {
            object o = archetypeChunk.GetComponent(component.Key, component.Value, entity.idEntity);
            entityDataCache.AddData(component.Key, o);
        }
        return entityDataCache;
    }

    public void RemoveEntity(ushort idEntity)
    {
        foreach (var item in listArchetypeChunks)
        {
            if (item.RemoveEntity(idEntity))
            {
                if (item.isEmpty)
                {
                    listArchetypeChunks.Remove(item);
                    if (listArchetypeChunks.Count == 0)
                    {
                        World.archetypeManager.listArchetypes.Remove(this);
                    }
                }
                break;
            }
        }
    }

    public ArchetypeChunk GetArchetypeChunk(Entity entity)
    {
        foreach (var chunk in listArchetypeChunks)
        {
            if (chunk.ContainEntity(entity.idEntity)) return chunk;
        }
        return null;
    }

    public byte GetComponentIndices(Component component) => componentIndices[component];
    public bool ContainFlag(Flag otherFlag)
    {
        return flag.ContainFlag(otherFlag);
    }
    public bool CompareArchetype(Flag otherFlag)
    {
        return flag.Equal(otherFlag);
    }
}