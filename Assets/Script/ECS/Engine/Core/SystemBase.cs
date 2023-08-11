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
    protected float deltaTime;

    #region R
    protected void NormalAction<R>(ActionR<R> action) where R : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<R>(ActionR<R> action) where R : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i]);
                }
            });
        }
    }

    protected void ParallelChunkAction<R>(ActionR<R> action, ActionR<R> normalAction) where R : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i]);
                }
            });
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                for (int i = 0; i < Chunk.count; i++)
                {
                    normalAction(ref componentArray_R.components[i]);
                }
            }
        }
    }
    #endregion

    #region I
    protected void NormalAction<I>(ActionI<I> action) where I : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_I = archetype.GetComponentIndices(entityQuery.components[0]);
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<I> componentArray_I = Chunk.GetComponentArray<I>(componentIndices_I);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_I.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<I>(ActionI<I> action) where I : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_I = archetype.GetComponentIndices(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<I> componentArray_I = Chunk.GetComponentArray<I>(componentIndices_I);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_I.components[i]);
                }
            });
        }
    }

    protected void ParallelChunkAction<I>(ActionI<I> action, ActionI<I> normalAction) where I : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_I = archetype.GetComponentIndices(entityQuery.components[0]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<I> componentArray_I = Chunk.GetComponentArray<I>(componentIndices_I);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_I.components[i]);
                }
            });
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<I> componentArray_R = Chunk.GetComponentArray<I>(componentIndices_I);
                for (int i = 0; i < Chunk.count; i++)
                {
                    normalAction(componentArray_R.components[i]);
                }
            }
        }
    }

    #endregion

    #region RR
    protected void NormalAction<R, R2>(ActionRR<R, R2> action) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            byte componentIndices_R2 = archetype.GetComponentIndices(entityQuery.components[1]);

            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                ComponentArray<R2> componentArray_R2 = Chunk.GetComponentArray<R2>(componentIndices_R2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i], ref componentArray_R2.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<R, R2>(ActionRR<R, R2> action) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            byte componentIndices_R2 = archetype.GetComponentIndices(entityQuery.components[1]);
            Parallel.ForEach(archetype.listArchetypeChunks, (Chunk =>
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                ComponentArray<R2> componentArray_R2 = Chunk.GetComponentArray<R2>(componentIndices_R2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i], ref componentArray_R2.components[i]);
                }
            }));
        }
    }

    protected void ParallelChunkAction<R, R2>(ActionRR<R, R2> action, ActionRR<R, R2> normalAction) where R : struct, IComponentData where R2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_R = archetype.GetComponentIndices(entityQuery.components[0]);
            byte componentIndices_R2 = archetype.GetComponentIndices(entityQuery.components[1]);
            Parallel.ForEach(archetype.listArchetypeChunks, Chunk =>
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                ComponentArray<R2> componentArray_R2 = Chunk.GetComponentArray<R2>(componentIndices_R2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(ref componentArray_R.components[i], ref componentArray_R2.components[i]);
                }
            });
            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<R> componentArray_R = Chunk.GetComponentArray<R>(componentIndices_R);
                ComponentArray<R2> componentArray_R2 = Chunk.GetComponentArray<R2>(componentIndices_R2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    normalAction(ref componentArray_R.components[i], ref componentArray_R2.components[i]);
                }
            }
        }
    }
    #endregion

    #region II
    protected void NormalAction<I, I2>(ActionII<I, I2> action) where I : struct, IComponentData where I2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_I = archetype.GetComponentIndices(entityQuery.components[0]);
            byte componentIndices_I2 = archetype.GetComponentIndices(entityQuery.components[1]);

            foreach (var Chunk in archetype.listArchetypeChunks)
            {
                ComponentArray<I> componentArray_I = Chunk.GetComponentArray<I>(componentIndices_I);
                ComponentArray<I2> componentArray_I2 = Chunk.GetComponentArray<I2>(componentIndices_I2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_I.components[i], componentArray_I2.components[i]);
                }
            }
        }
    }

    protected void ParallelChunkAction<I, I2>(ActionII<I, I2> action) where I : struct, IComponentData where I2 : struct, IComponentData
    {
        foreach (var archetype in World.archetypeManager.GetArchetypeForQuery(entityQuery.flag))
        {
            byte componentIndices_I = archetype.GetComponentIndices(entityQuery.components[0]);
            byte componentIndices_I2 = archetype.GetComponentIndices(entityQuery.components[1]);
            Parallel.ForEach(archetype.listArchetypeChunks, (Chunk =>
            {
                ComponentArray<I> componentArray_I = Chunk.GetComponentArray<I>(componentIndices_I);
                ComponentArray<I2> componentArray_I2 = Chunk.GetComponentArray<I2>(componentIndices_I2);
                for (int i = 0; i < Chunk.count; i++)
                {
                    action(componentArray_I.components[i], componentArray_I2.components[i]);
                }
            }));
        }
    }

    #endregion
}