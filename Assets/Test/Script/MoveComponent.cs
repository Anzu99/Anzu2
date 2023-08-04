using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public MoveData moveData;
}

[System.Serializable]
public struct MoveData
{
    public Transform transform;
    public Vector3 direction;
    public float speed;
}