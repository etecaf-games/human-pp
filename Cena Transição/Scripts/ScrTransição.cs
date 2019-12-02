using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrTransição : MonoBehaviour
{
    public GameObject[] Marcas;
    public float ContagemLigado = 1;
    public float ContagemDesligado = 1;
    float Redutor = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ContagemLigado -= Redutor * Time.deltaTime;
        if (ContagemLigado <= 0)
        {
            Marcas[0].SetActive(false);
            ContagemDesligado -= Redutor * Time.deltaTime;
            if (ContagemDesligado <= 0)
            {
                Marcas[0].SetActive(true);
                ContagemLigado = 1;
                ContagemDesligado = 1;
            }

        }
    }
}
