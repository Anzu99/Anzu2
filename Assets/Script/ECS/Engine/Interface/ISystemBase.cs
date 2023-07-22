public interface ISystemBase
{
    void Awake();
    void Start();
    void Update();
    void FixedUpdate();

    public delegate void ActionRR<R, R2>(ref R arg1, ref R2 arg2);
    public delegate void ActionRI<R, I>(ref R arg1, in I arg2);

    public delegate void ActionRRR<R, R2, R3>(ref R arg1, ref R2 arg2, ref R3 arg3);
    public delegate void ActionRRI<R, R2, I>(ref R arg1, ref R2 arg2, in I arg3);
    public delegate void ActionRII<R, I, I2>(ref R arg1, in I arg2, in I2 arg3);

    void ForEach<R, R2, R3>(ActionRRR<R, R2, R3> action) where R : struct, IComponentData where R2 : struct, IComponentData where R3 : struct, IComponentData;
}