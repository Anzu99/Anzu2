using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Info info;
    private void Awake()
    {
        info = GetComponent<Info>();
        Initialize();
    }
    public void Initialize()
    {
        float x = Random.Range(2.75f, -2.75f);
        float y = Random.Range(5.0f, -5.0f);
        transform.position = new Vector3(x, y, 0);
    }
    private float waveSpeed = 5;
    private float cycle;

    float deltaTime;
    private void Update()
    {
        deltaTime = Time.deltaTime;
        cycle += waveSpeed * deltaTime;
        float sinval = Mathf.Sin(cycle);
        if (info.direction == Info.Direction.Horizontal)
        {
            if (info.invert)
            {
                transform.position += Vector3.left * deltaTime * info.speed + Vector3.up * sinval * deltaTime;
                if (transform.position.x <= info.xRange.x)
                {
                    info.invert = false;
                }
            }
            else
            {
                transform.position += Vector3.right * deltaTime * info.speed + Vector3.up * sinval * deltaTime;
                if (transform.position.x >= info.xRange.y)
                {
                    info.invert = true;
                }
            }
        }
        else
        {
            if (info.invert)
            {
                transform.position += Vector3.down * deltaTime * info.speed + Vector3.right * sinval * deltaTime;
                if (transform.position.y <= info.yRange.x)
                {
                    info.invert = false;
                }
            }
            else
            {
                transform.position += Vector3.up * deltaTime * info.speed + Vector3.right * sinval * deltaTime; ;
                if (transform.position.y >= info.yRange.y)
                {
                    info.invert = true;
                }
            }
        }
    }
}
