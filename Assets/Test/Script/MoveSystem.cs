using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class MoveSystem : MonoBehaviour
{
    MoveData[] moveDatas;
    [SerializeField] private MoveComponent movePrefab;


    [BurstCompile]
    struct MoveJob : IJobParallelForTransform
    {
        public float deltaTime;
        [ReadOnly] public NativeArray<MoveData> nativeArrayMoveDatas;

        public void Execute(int index, TransformAccess transform)
        {
            MoveData moveData = nativeArrayMoveDatas[index];
            transform.position += moveData.direction * moveData.speed * deltaTime;
        }
    }


    private void Start()
    {
        int count = 1000;
        moveDatas = new MoveData[count];
        for (var i = 0; i < count; i++)
        {
            MoveComponent moveComponent = Instantiate(movePrefab);
            moveDatas[i] = moveComponent.moveData;
        }
    }

    void Update()
    {
        NativeArray<MoveData> nativeMyMoveData = new NativeArray<MoveData>(moveDatas, Allocator.TempJob);

        TransformAccessArray transformAccessArray = new TransformAccessArray(moveDatas.Length);
        for (int i = 0; i < moveDatas.Length; i++)
        {
            transformAccessArray.Add(moveDatas[i].transform);
        }

        MoveJob moveJob = new MoveJob
        {
            deltaTime = Time.deltaTime,
            nativeArrayMoveDatas = nativeMyMoveData
        };

        JobHandle jobHandle = moveJob.Schedule(transformAccessArray);

        jobHandle.Complete();

        nativeMyMoveData.Dispose();
        transformAccessArray.Dispose();
    }
}


