using UnityEngine;

public struct InfoComponent : IComponentData
{
    private ushort idEntity;
    public ushort IdEntity { get => idEntity; set => idEntity = value; }


    public float speed;
    public int laps;

    public Vector2 xRange;
    public Vector2 yRange;

    public Direction direction;
    public bool invert;
    public void Initialize()
    {
        invert = Random.Range(0, 2) == 0;
        direction = invert ? Direction.Horizontal : Direction.Vertivcal;

        xRange = new Vector2(-2.75f, 2.75f);
        yRange = new Vector2(-5.0f, 5.0f);

        speed = Random.Range(3.0f, 10.0f);
        laps = 0;
    }

    public enum Direction
    {
        Horizontal,
        Vertivcal
    }
}
