using UnityEngine;

public struct MovementComponent : IComponentData
{
    public ushort idEntity;
    public ushort IdEntity { get => idEntity; set => idEntity = value; }

    public Vector3 currentPosition;
    public Transform transform;
    public void Initialize()
    {
        // transform = World.GetEntity(idEntity).gameObject.transform;
        float x = Random.Range(2.75f, -2.75f);
        float y = Random.Range(5.0f, -5.0f);
        transform.position = new Vector3(x, y, 0);
    }
}
