using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

public class Archetype
{
    public Flag flag;
    List<ArchetypeChunk> listArchetypeChunks;
    public ushort chunkSize;
    Component[] components;
    Dictionary<Component, byte> componentIndices;
    public Archetype(Component[] components, Flag flag)
    {
        componentIndices = new Dictionary<Component, byte>();
        listArchetypeChunks = new List<ArchetypeChunk>();
        CreateNewChunk();
        this.flag = flag;
        this.components = components;
    }


    public void AddEntity(Entity entity)
    {
        if (listArchetypeChunks[^1].count < chunkSize)
        {
            listArchetypeChunks[^1].AddEntity(entity);
        }
        else
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

    public IEnumerable<NativeArray<T>> GetComponentArrayData<T>(Component component) where T : struct, IComponentData
    {
        foreach (var item in listArchetypeChunks)
        {
            yield return item.GetComponentArray<T>(componentIndices[component]);
        }
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
    Dictionary<ushort, ushort> entityIndices;
    IcomponentArray[] componentArrays;
    public ushort count;

    public ArchetypeChunk(Component[] components, ushort chunkSize)
    {
        count = 1;
        entityIndices = new Dictionary<ushort, ushort>();
        componentArrays = new IcomponentArray[components.Length];
        for (var i = 0; i < components.Length; i++)
        {
            CreateComponentArray(components[i], chunkSize, i);
        }
    }
    public NativeArray<T> GetComponentArray<T>(byte componentidx) where T : struct, IComponentData
    {
        ComponentArray<T> componentArray = (ComponentArray<T>)componentArrays[componentidx];
        return componentArray.components;
    }

    public void AddEntity(Entity entity)
    {
        for (var i = 0; i < componentArrays.Length; i++)
        {
            componentArrays[count - 1].CreateComponent(entity);
            entityIndices.Add(entity.idEntity, (ushort)(count - 1));
            count++;
        }
    }

    public void RemoveEntity(Entity entity)
    {
        count--;
        entityIndices.Remove(entity.idEntity);
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









public class Manager
{
    object[] itemArrays;

    private void Start()
    {
        itemArrays = new object[10];

        for (var i = 0; i < itemArrays.Length; i++)
        {
            itemArrays[i] = new ItemArray<ItemBuff>();
        }
    }

    public void Execute()
    {
        foreach (var item in itemArrays)
        {
            ItemArray<ItemBuff> itemArray = (ItemArray<ItemBuff>)item;
        }

        MyJob job = new MyJob
        {
        };
    }

    struct MyJob : IJobParallelFor
    {
        public NativeArray<ItemBuff> resultArray;

        public void Execute(int index)
        {
            resultArray[index].PickUp();
        }
    }
}




public class ItemArray<T> where T : struct, IItem
{
    public NativeArray<T> items;

    public ItemArray()
    {
        items = new NativeArray<T>(100, Allocator.Temp);
    }
    public void PickUp()
    {
        foreach (var item in items)
        {
            item.PickUp();
        }
    }
}

public struct ItemBuff : IItem
{
    public void PickUp()
    {
        UnityEngine.Debug.LogError("pick up");
    }
}

public interface IItem
{
    public void PickUp();
}