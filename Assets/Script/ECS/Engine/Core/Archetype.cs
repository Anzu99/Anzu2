using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using Unity.Jobs;

public class Archetype
{
    public Flag flag;
    [ShowInInspector] public List<ArchetypeChunk> listArchetypeChunks;
    private ushort chunkSize = 5;
    private Component[] components;
    private Dictionary<Component, byte> componentIndices;
    public bool isEmpty => listArchetypeChunks.Count <= 0;
    public Archetype(Flag flag)
    {
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

    public ComponentArray<T>[] GetComponentArrayData<T>(Component component) where T : struct, IComponentData
    {
        int count = listArchetypeChunks.Count;
        ComponentArray<T>[] componentArrays = new ComponentArray<T>[count];
        for (int i = 0; i < count; i++)
        {
            componentArrays[i] = listArchetypeChunks[i].GetComponentArray<T>(componentIndices[component]);
        }
        return componentArrays;
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

    public void RemoveEntity(ushort idEntity)
    {
        foreach (var item in listArchetypeChunks)
        {
            if (item.RemoveEntity(idEntity))
            {
                if (item.isEmpty)
                {
                    // item.DestroyChunk();
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

