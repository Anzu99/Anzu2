public class Archetype
{
    public Flag flag;
    ArchetypeChunk[] archetypeChunks;
    public ushort chunkSize;


    public Archetype(Flag flag)
    {

    }

    public void AddEntity(ushort idEntity)
    {

    }

    public void RemoveEntity(ushort idEntity)
    {

    }

    public void RemoveArchetype()
    {
        flag.Clear();
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
    public ArchetypeChunk()
    {

    }

    private void CreateComponentArray()
    {

    }
    private void CreateEntity()
    {
        
    }
    ComponentArray[] componentArrays;
    public Entity[] entities;
}
