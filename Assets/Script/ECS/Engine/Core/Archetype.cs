public class Archetype
{
    public Flag flag;
    public ushort count;
    public ushort[] entities;
    public ushort[] indice;

    public Archetype(Flag flag)
    {
        indice = new ushort[ConfigCapacity.MaxEnities];
        entities = new ushort[ConfigCapacity.MaxEnities];
        this.flag = flag;
        count = 0;
    }

    public void AddEntity(ushort idEntity)
    {
        entities[++count] = idEntity;
        indice[idEntity] = count;
    }

    public void RemoveEntity(ushort idEntity)
    {
        ushort tmp = entities[count];
        entities[indice[idEntity]] = tmp;
        entities[count] = 0;
        if (--count <= 0)
        {
            World.archetypeManager.RemoveArchetype(this);
            RemoveArchetype();
        }
    }

    public void RemoveArchetype()
    {
        flag.Clear();
        for (var i = 0; i < count; i++)
        {
            entities[i] = 0;
            indice[i] = 0;
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