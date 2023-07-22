using System;

public class ComponentArray<T> where T : struct, IComponentData
{
    public short count;
    public short[] entitiesID;
    public T[] components;

    public ComponentArray(ushort _maxCapacity)
    {
        count = 0;
        components = new T[_maxCapacity];
        entitiesID = new short[ConfigCapacity.MaxEnities];
        Array.Fill(entitiesID, (short)-1);
    }

    public void CreateComponent(ushort idEntity)
    {
        try
        {
            components[count].IdEntity = idEntity;
            entitiesID[idEntity] = count++;
        }
        catch (Exception ex)
        {
            if (count >= components.Length)
            {
                UnityEngine.Debug.LogError($"Component array {typeof(T)} Overload, config capacity {typeof(T)} to fix");
            }
            else
            {
                UnityEngine.Debug.LogError(ex.Message);
            }
        }

    }

    public void InitializeComponent(ushort idEntity)
    {
        components[entitiesID[idEntity]].Initialize();
    }

    public void RemoveComponent(ushort idEntity)
    {
        ushort entityIDTmp = components[count].IdEntity;
        components[entitiesID[idEntity]].IdEntity = entityIDTmp;
        count--;
    }

    public ref T GetComponentFromArray(ushort idEntity)
    {
        return ref components[entitiesID[idEntity]];
    }
    public ref T[] GetComponentArray()
    {
        return ref components;
    }


}