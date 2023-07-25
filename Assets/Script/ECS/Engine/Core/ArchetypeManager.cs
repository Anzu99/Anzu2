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
        Archetype archetype = GetArchetype(entity.flag);
        archetype.AddEntity(entity);
    }

    public IEnumerable<ushort> GetIDEntitiesForQuery(Flag flag)
    {
        
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
        for (var i = 0; i < count; i++)
        {
            if (listArchetypes[i].CompareArchetype(flag)) return listArchetypes[i];
        }
        return CreateArchetype(flag);
    }

    public void ChangeEntityArchetype(Entity entity, Flag newflag)
    {
        GetArchetype(entity.flag).RemoveEntity(entity.idEntity);
        GetArchetype(newflag).AddEntity(entity.idEntity);
    }

    public void RemoveArchetype(Archetype archetype)
    {
        for (var i = 0; i < count; i++)
        {
            if (listArchetypes[i] == archetype)
            {
                if (i == count - 1)
                {
                    count--;
                    break;
                }
                Archetype tmp = listArchetypes[count - 1];
                listArchetypes[i] = tmp;
                count--;
                break;
            }
        }
    }

    /* =========================================== LOCAL FUNCTION =========================================== */
    private Archetype CreateArchetype(Flag flag)
    {
        listArchetypes[count] = new Archetype(flag);
        return listArchetypes[count++];
    }



}

