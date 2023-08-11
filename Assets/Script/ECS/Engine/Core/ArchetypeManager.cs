using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;

public class ArchetypeManager
{
    public List<Archetype> listArchetypes;

    public ArchetypeManager()
    {
        listArchetypes = new List<Archetype>();
    }
    public IEnumerable<Archetype> GetArchetypeForQuery(Flag flag)
    {
        foreach (var archetype in listArchetypes)
        {
            if (archetype.ContainFlag(flag)) yield return archetype;
        }
    }

    public Archetype GetArchetype(Flag flag, ushort chunkSize = 0)
    {
        foreach (var item in listArchetypes)
        {
            if (item.CompareArchetype(flag)) return item;
        }
        return CreateArchetype(flag, chunkSize);
    }

    public void EntityChangeArchetype(Entity entity, Flag newFlag, ushort chunkSize = 0)
    {
        EntityDataCache entityDataCopy = entity.archetype.GetEntityDataCopy(entity);
        entity.archetype.RemoveEntity(entity.idEntity);
        GetArchetype(newFlag, chunkSize).AddEntity(entity, entityDataCopy);
    }

    private Archetype CreateArchetype(Flag flag, ushort chunkSize)
    {
        Archetype archetype = new Archetype(flag, chunkSize);
        listArchetypes.Add(archetype);
        return archetype;
    }
}

