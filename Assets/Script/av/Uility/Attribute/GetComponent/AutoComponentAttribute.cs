using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoComponentAttribute : PropertyAttribute
{
    public readonly Type type;
    public AutoComponentAttribute(Type t)
    {
        type = t;
    }
}
