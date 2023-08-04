using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

public class ArchetypeManager
{
    public List<Archetype> listArchetypes;


    public ArchetypeManager()
    {
        listArchetypes = new List<Archetype>();
    }

    public void AddToArchetype(Entity entity)
    {
        entity.archetype.AddEntity(entity);
    }

    public IEnumerable<Archetype> GetArchetypeForQuery(Flag flag)
    {
        foreach (var archetype in listArchetypes)
        {
            if (archetype.ContainFlag(flag)) yield return archetype;
        }
    }

    public Archetype GetArchetype(Flag flag)
    {
        foreach (var item in listArchetypes)
        {
            if (item.CompareArchetype(flag)) return item;
        }
        return CreateArchetype(flag);
    }

    public void ChangeEntityArchetype(Entity entity, Flag newflag)
    {
        entity.archetype.RemoveEntity(entity.idEntity);
        GetArchetype(newflag).AddEntity(entity);
    }

    public void RemoveEntity(Flag flag, Entity entity)
    {
        Archetype archetype = GetArchetype(flag);
        archetype.RemoveEntity(entity.idEntity);
        if (archetype.isEmpty)
        {
            listArchetypes.Remove(archetype);
        }
    }

    public Entity GetEntity(ushort idEntity)
    {
        foreach (var archetype in listArchetypes)
        {
            Entity entity = archetype.GetEntity(idEntity);
            if (entity != null) return entity;
        }
        return null;
    }

    /* =========================================== LOCAL FUNCTION =========================================== */
    private Archetype CreateArchetype(Flag flag)
    {
        Archetype archetype = new Archetype(flag);
        listArchetypes.Add(archetype);
        return archetype;
    }
}

