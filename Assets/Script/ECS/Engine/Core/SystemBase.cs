using UnityEngine;

public class SystemBase
{
    protected World world;
    protected EntityQuery entityQuery;
    public bool active = true;
    public SystemBase() { }
    public void Update() { if (active) Execute(); }
    public virtual void Start() { }
    public virtual void Execute() { }
    public virtual void FixedUpdate() { }

    public delegate void ActionR<R>(ref R arg1);
    public delegate void ActionI<I>(in I arg1);


    public delegate void ActionRR<R, R2>(ref R arg1, ref R2 arg2);
    public delegate void ActionRI<R, I>(ref R arg1, in I arg2);
    public delegate void ActionII<I, I2>(in I arg1, in I2 arg2);


    public delegate void ActionRRR<R, R2, R3>(ref R arg1, ref R2 arg2, ref R3 arg3);
    public delegate void ActionRRI<R, R2, I>(ref R arg1, ref R2 arg2, in I arg3);
    public delegate void ActionRII<R, I, I2>(ref R arg1, in I arg2, in I2 arg3);
    public delegate void ActionIII<I, I2, I3>(in I arg1, in I2 arg2, in I3 arg3);


    public delegate void ActionRRRR<R, R2, R3, R4>(ref R arg1, ref R2 arg2, ref R3 arg3, ref R4 arg4);
    public delegate void ActionRRRI<R, R2, R3, I>(ref R arg1, ref R2 arg2, ref R3 arg3, in I arg4);
    public delegate void ActionRRII<R, R2, I, I2>(ref R arg1, ref R2 arg2, in I arg3, in I2 arg4);
    public delegate void ActionRIII<R, I, I2, I3>(ref R arg1, in I arg2, in I2 arg3, in I3 arg4);
    public delegate void ActionIIII<I, I2, I3, I4>(in I arg1, in I2 arg2, in I3 arg3, in I4 arg4);



    #region P
    /* =========================================== P =========================================== */
    protected void ForEach<R>(ActionR<R> action) where R : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        for (int i = 0; i < World.componentManager.GetComponentsLenght<R>(entityQuery.components[0]); i++)
        {
            ref R c1 = ref World.componentManager.GetComponents<R>(entityQuery.components[0])[i];
            action(ref c1);
        }
    }
    protected void ForEach<I>(ActionI<I> action) where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        for (int i = 0; i < World.componentManager.GetComponentsLenght<I>(entityQuery.components[0]); i++)
        {
            action(World.componentManager.GetComponents<I>(entityQuery.components[0])[i]);
        }
    }

    protected void ForEach<R>(EntityQuery query, ActionR<R> action) where R : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(query.components[0])) return;
        for (int i = 0; i < World.componentManager.GetComponentsLenght<R>(query.components[0]); i++)
        {
            ref R c1 = ref World.componentManager.GetComponents<R>(query.components[0])[i];
            action(ref c1);
        }
    }
    protected void ForEach<I>(EntityQuery query, ActionI<I> action) where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(query.components[0])) return;
        for (int i = 0; i < World.componentManager.GetComponentsLenght<I>(query.components[0]); i++)
        {
            action(World.componentManager.GetComponents<I>(query.components[0])[i]);
        }
    }
    /* =========================================== P =========================================== */
    #endregion

    #region PP
    /* =========================================== PP =========================================== */
    protected void ForEach<R, R2>(ActionRR<R, R2> action) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
            action(ref c1, ref c2);
        }
    }

    protected void ForEach<R, I>(ActionRI<R, I> action) where R : struct, IComponentData where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            I c2 = World.componentManager.GetComponent<I>(entityQuery.components[1], item);
            action(ref c1, c2);
        }
    }
    protected void ForEach<I, I2>(ActionII<I, I2> action) where I : struct, IComponentData where I2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            I c1 = World.componentManager.GetComponent<I>(entityQuery.components[0], item);
            I2 c2 = World.componentManager.GetComponent<I2>(entityQuery.components[1], item);
            action(c1, c2);
        }
    }


    protected void ForEach<R, R2>(EntityQuery query, ActionRR<R, R2> action) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(query.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(query.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(query.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(query.components[1], item);
            action(ref c1, ref c2);
        }
    }
    protected void ForEach<R, I>(EntityQuery query, ActionRI<R, I> action) where R : struct, IComponentData where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(query.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(query.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(query.components[0], item);
            I c2 = World.componentManager.GetComponent<I>(query.components[1], item);
            action(ref c1, c2);
        }
    }
    protected void ForEach<I, I2>(EntityQuery query, ActionII<I, I2> action) where I : struct, IComponentData where I2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(query.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(query.flag))
        {
            I c1 = World.componentManager.GetComponent<I>(query.components[0], item);
            I2 c2 = World.componentManager.GetComponent<I2>(query.components[1], item);
            action(c1, c2);
        }
    }

    /* =========================================== PP =========================================== */
    #endregion

    #region PPP
    /* =========================================== PPP =========================================== */
    public void ForEach<R, R2, R3>(ActionRRR<R, R2, R3> action) where R : struct, IComponentData where R2 : struct, IComponentData where R3 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        if (!World.componentManager.HasComponentArray(entityQuery.components[1])) return;
        if (!World.componentManager.HasComponentArray(entityQuery.components[2])) return;

        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
            ref R3 c3 = ref World.componentManager.GetComponent<R3>(entityQuery.components[2], item);

            action(ref c1, ref c2, ref c3);
        }
    }
    protected void ForEach<R, R2, I>(ActionRRI<R, R2, I> action) where R : struct, IComponentData where R2 : struct, IComponentData where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
            I c3 = World.componentManager.GetComponent<I>(entityQuery.components[2], item);
            action(ref c1, ref c2, c3);
        }
    }
    protected void ForEach<R, I, I2>(ActionRII<R, I, I2> action) where R : struct, IComponentData where I : struct, IComponentData where I2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            I c2 = World.componentManager.GetComponent<I>(entityQuery.components[1], item);
            I2 c3 = World.componentManager.GetComponent<I2>(entityQuery.components[2], item);
            action(ref c1, c2, c3);
        }
    }
    protected void ForEach<I, I2, I3>(ActionIII<I, I2, I3> action) where I : struct, IComponentData where I2 : struct, IComponentData where I3 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            I c1 = World.componentManager.GetComponent<I>(entityQuery.components[0], item);
            I2 c2 = World.componentManager.GetComponent<I2>(entityQuery.components[1], item);
            I3 c3 = World.componentManager.GetComponent<I3>(entityQuery.components[2], item);
            action(c1, c2, c3);
        }
    }
    //============================================================================================
    public void ForEach<R, R2, R3>(EntityQuery entityQuery, ActionRRR<R, R2, R3> action) where R : struct, IComponentData where R2 : struct, IComponentData where R3 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        if (!World.componentManager.HasComponentArray(entityQuery.components[1])) return;
        if (!World.componentManager.HasComponentArray(entityQuery.components[2])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
            ref R3 c3 = ref World.componentManager.GetComponent<R3>(entityQuery.components[2], item);

            action(ref c1, ref c2, ref c3);
        }
    }
    protected void ForEach<R, R2, I>(EntityQuery entityQuery, ActionRRI<R, R2, I> action) where R : struct, IComponentData where R2 : struct, IComponentData where I : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
            I c3 = World.componentManager.GetComponent<I>(entityQuery.components[2], item);
            action(ref c1, ref c2, c3);
        }
    }
    protected void ForEach<R, I, I2>(EntityQuery entityQuery, ActionRII<R, I, I2> action) where R : struct, IComponentData where I : struct, IComponentData where I2 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
            I c2 = World.componentManager.GetComponent<I>(entityQuery.components[1], item);
            I2 c3 = World.componentManager.GetComponent<I2>(entityQuery.components[2], item);
            action(ref c1, c2, c3);
        }
    }
    protected void ForEach<I, I2, I3>(EntityQuery entityQuery, ActionIII<I, I2, I3> action) where I : struct, IComponentData where I2 : struct, IComponentData where I3 : struct, IComponentData
    {
        if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        {
            I c1 = World.componentManager.GetComponent<I>(entityQuery.components[0], item);
            I2 c2 = World.componentManager.GetComponent<I2>(entityQuery.components[1], item);
            I3 c3 = World.componentManager.GetComponent<I3>(entityQuery.components[2], item);
            action(c1, c2, c3);
        }
    }
    /* =========================================== PPP =========================================== */
    #endregion
}