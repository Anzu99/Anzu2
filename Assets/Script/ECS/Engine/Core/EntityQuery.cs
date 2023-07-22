using System;
using System.Collections;
using System.Collections.Generic;

public class EntityQuery
{
    public Flag flag;
    public Component[] components;
    public EntityQuery(params Component[] _Components)
    {
        flag.Init();
        components = _Components;
        flag.AddComponents(components);
    }
}