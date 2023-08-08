using System.Threading.Tasks;
using System;
using UnityEngine;

public class SystemBase
{
    protected World world;
    protected EntityQuery entityQuery;
    public bool active = true;
    public SystemBase() { }
    public void Update()
    {
        if (active)
        {
            deltaTime = Time.deltaTime;
            Execute();
        }
    }
    public virtual void Start() { }
    public virtual void Execute() { }
    public virtual void FixedUpdate() { }

    public delegate void Action1P<P>(P arg1);
    public delegate void ActionR<R>(ref R arg1);
    public delegate void ActionI<I>(in I arg1);

    public delegate void Action2P<P, P2>(P arg1, P2 arg2);
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
    protected float deltaTime;

    #region P
    protected void NormalAction<P>(Action1P<P> action) where P : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<P>(Action1P<P> action) where P : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i]);
                }
            });
        }
    }

    protected void ParallelChunkAction<P>(Action1P<P> action, Action1P<P> normalAction) where P : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i]);
                }
            });
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<P> componentArray_R = Chunk.GetComponentArray<P>(componentIndices_P);
                for (int i = 0; i < Chunk.count; i++)
                {
                    normalAction(componentArray_R.components[i]);
                }
            }
        }
    }

    #endregion

    #region PP
    /* =========================================== PP =========================================== */
    protected void NormalAction<P, P2>(Action2P<P, P2> action) where P : struct, IComponentData where P2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            byte componentIndices_P2 = archetype.GetComponentIndice(entityQuery.components[1]);

            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                ComponentArray<P2> componentArray_P2 = Chunk.GetComponentArray<P2>(componentIndices_P2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i], componentArray_P2.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<P, P2>(Action2P<P, P2> action) where P : struct, IComponentData where P2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            byte componentIndices_P2 = archetype.GetComponentIndice(entityQuery.components[1]);
            Parallel.ForEach(archetype.listArchetypeChunks, (Action<ArchetypeChunk>)(Chunk =>
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                ComponentArray<P2> componentArray_P2 = Chunk.GetComponentArray<P2>(componentIndices_P2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i], componentArray_P2.components[i]);
                }
            }));
        }
    }

    protected void ParallelChunkAction<P, P2>(Action2P<P, P2> action, Action2P<P, P2> normalAction) where P : struct, IComponentData where P2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_P = archetype.GetComponentIndice(entityQuery.components[0]);
            byte componentIndices_P2 = archetype.GetComponentIndice(entityQuery.components[1]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<P> componentArray_P = Chunk.GetComponentArray<P>(componentIndices_P);
                ComponentArray<P2> componentArray_P2 = Chunk.GetComponentArray<P2>(componentIndices_P2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_P.components[i], componentArray_P2.components[i]);
                }
            });
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<P> componentArray_R = Chunk.GetComponentArray<P>(componentIndices_P);
                ComponentArray<P2> componentArray_R2 = Chunk.GetComponentArray<P2>(componentIndices_P2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    normalAction(componentArray_R.components[i], componentArray_R2.components[i]);
                }
            }
        }
    }

    /* =========================================== PP =========================================== */
    #endregion

}