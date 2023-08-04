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


    #region PP
    /* =========================================== PP =========================================== */
    protected void ForEach<R, R2>(ActionRR<R, R2> action) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        // if (!World.componentManager.HasComponentArray(entityQuery.components[0])) return;
        // foreach (var item in World.archetypeManager.GetIDEntitiesForQuery(entityQuery.flag))
        // {
        //     ref R c1 = ref World.componentManager.GetComponent<R>(entityQuery.components[0], item);
        //     ref R2 c2 = ref World.componentManager.GetComponent<R2>(entityQuery.components[1], item);
        //     action(ref c1, ref c2);
        // }
    }

    protected void ForEach<R, I>(ActionRI<R, I> action) where R : struct, IComponentData where I : struct, IComponentData
    {
       
    }
    /* =========================================== PP =========================================== */
    #endregion

}