using System;
using System.Collections;
using System.Collections.Generic;

public class ArchetypeManager
{
    public int count;
    public Archetype[] archetypes;

    public ArchetypeManager()
    {
        count = 0;
        archetypes = new Archetype[ConfigCapacity.MaxArchetype];
    }

    public void AddToArchetype(Entity entity)
    {
        Archetype archetype = GetArchetype(entity.flag);
        archetype.AddEntity(entity.idEntity);
    }

    public IEnumerable<ushort> GetIDEntitiesForQuery(Flag flag)
    {
        for (var i = 0; i < count; i++)
        {
            if (archetypes[i].ContainFlag(flag))
            {
                for (int j = 1; j <= archetypes[i].count; j++)
                {
                    yield return archetypes[i].entities[j];
                }
            }
        }
    }

    public Archetype GetArchetype(Flag flag)
    {
        for (var i = 0; i < count; i++)
        {
            if (archetypes[i].CompareArchetype(flag)) return archetypes[i];
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
            if (archetypes[i] == archetype)
            {
                if (i == count - 1)
                {
                    count--;
                    break;
                }
                Archetype tmp = archetypes[count - 1];
                archetypes[i] = tmp;
                count--;
                break;
            }
        }
    }

    /* =========================================== LOCAL FUNCTION =========================================== */
    private Archetype CreateArchetype(Flag flag)
    {
        archetypes[count] = new Archetype(flag);
        return archetypes[count++];
    }
}


public class ArchetypeChunk
{

}