using UnityEngine;

[System.Serializable]
public struct Flag
{
    public int[] components;

    public void Init()
    {
        components = new int[2];
    }

    public void Copy(int[] value)
    {
        components = value;
    }

    public void Clear()
    {
        for (var i = 0; i < components.Length; i++) components[i] = 0;
    }


    public static Flag operator +(Flag a, Flag b)
    {
        return new Flag();
    }
    public static Flag operator -(Flag a, Flag b)
    {
        return new Flag();
    }
    public static Flag operator +(Flag a, Component b)
    {
        return new Flag();
    }
    public static Flag operator -(Flag a, Component b)
    {
        return new Flag();
    }

    public void AddComponent(Component eComponent)
    {
        components.AddFlag(eComponent);
    }
    public void RemoveComponent(Component eComponent)
    {
        components.RemoveFlag(eComponent);
    }

    public void AddComponents(params Component[] eComponents)
    {
        foreach (var item in eComponents)
        {
            components.AddFlag(item);
        }
    }

    public void RemoveComponents(params Component[] eComponents)
    {
        foreach (var item in eComponents)
        {
            components.RemoveFlag(item);
        }
    }

    public bool Equal(Flag value)
    {
        return value.Equal(components);
    }

    private bool Equal(params int[] value)
    {
        for (var i = 0; i < value.Length; i++)
        {
            if (value[i] != components[i]) return false;
        }
        return true;
    }

    public bool ContainComponent(Component eComponent)
    {
        return components.ContainComponent(eComponent);
    }

    public bool ContainComponents(params Component[] eComponents)
    {
        foreach (var item in eComponents)
        {
            if (!components.ContainComponent(item)) return false;
        }
        return true;
    }

    public bool ContainFlag(Flag otherFlag)
    {
        for (var i = 0; i < otherFlag.components.Length; i++)
        {
            if (!components[i].ContainFlag(otherFlag.components[i])) return false;
        }
        return true;
    }
}