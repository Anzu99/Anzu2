using System;
using System.Collections.Generic;
using AV;
using UnityEngine;

public struct Flag
{
    public int[] components;

    public void Init()
    {
        components = new int[2];
    }

    public void Copy(Flag otherFlag)
    {
        if (components == null) Init();
        for (int i = 0; i < otherFlag.components.Length; i++)
        {
            components[i] = otherFlag.components[i];
        }
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
        Flag tmp =new Flag();
        tmp.Copy(a);
        tmp.AddComponent(b);
        return tmp;
    }

    public static Flag operator -(Flag a, Component b)
    {
        Flag tmp =new Flag();
        tmp.Copy(a);
        tmp.RemoveComponent(b);
        return tmp;
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
        if (components == null) Init();
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
        return value.components.CompareFlags(components);
    }

    // private bool Equal(params int[] value)
    // {
    //     for (var i = 0; i < value.Length; i++)
    //     {
    //         if (value[i] != components[i]) return false;
    //     }
    //     return true;
    // }

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

    public Component[] ToComponents()
    {
        List<Component> listComponent = new List<Component>();

        Component componentTmp = Component.None;
        for (var i = 1; i < Enum.GetNames(typeof(Component)).Length; i++)
        {
            componentTmp = AV_ENUM_EXTENSION.Next(componentTmp);
            if (ContainComponent(componentTmp))
            {
                listComponent.Add(componentTmp);
            }
        }

        return listComponent.ToArray();
    }
}