using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using AV;
public class ComparePer : MonoBehaviour
{
    void Execute()
    {

        AV_WATCH.Start("2");
        for (var i = 0; i < 100; i++)
        {
        }
        AV_WATCH.Stop("2");


        AV_WATCH.Start("1");
        for (var i = 0; i < 100; i++)
        {
        }
        AV_WATCH.Stop("1");

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { Execute(); }
    }
}
