using UnityEngine;

public class ObjMoveTest : MonoBehaviour
{
    public float speed;
    private float speed1;
    private float speed2;
    private float speed3;
    private float speed4;
    private void Awake()
    {
    }
    private void Update()
    {
        for (int i = 0; i < 200; i++)
        {
            speed = 1;
            speed *= 2;
            speed = Mathf.Pow(speed, 10) / 10000000;
        }
        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}