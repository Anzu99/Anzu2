using System.Collections;
using System.Collections.Generic;
using ANZU;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private Button btnAdd;
    [SerializeField] private Text txtCount;
    private void Awake()
    {
        btnAdd.onClick.AddListener(AddObject);
    }
    int count = 0;
    private void Start()
    {
        ShowFPS.ShowFPSHandle();
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(.5f);
            int index = 0;
            while (index < 5000)
            {
                yield return null;
                for (var i = 0; i < 40; i++)
                {
                    Instantiate(Resources.Load<GameObject>(PathConfig.Entity.player2)).name = index.ToString();
                    index++;
                }
                count += 40;
                txtCount.text = count.ToString();
            }
        }
    }
    void AddObject()
    {
        StartCoroutine(Spawn());
        IEnumerator Spawn()
        {
            yield return new WaitForSeconds(.5f);
            int index = count;
            int max = count + 500;
            while (index < max)
            {
                yield return null;
                for (var i = 0; i < 20; i++)
                {
                    Instantiate(Resources.Load<GameObject>(PathConfig.Entity.player2)).name = index.ToString();
                    index++;
                }
                count += 20;
                txtCount.text = count.ToString();
            }
        }
    }
}
