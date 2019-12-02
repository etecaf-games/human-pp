//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class vidainimigo : MonoBehaviour
{
    public GameObject UIinimigo;
    public Slider Vidaini;
    public float timer,initimer=3f;
    public Image rosto;
 //   public Sprite[] rostos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=initimer)
        {
            UIinimigo.SetActive(false);
            timer = 0;

        }
    }
    public void inimigoVida( int vidaatual)
    {
        Vidaini.maxValue = 100;
        Vidaini.value = vidaatual;
        timer = 0;
        UIinimigo.SetActive(true);
    }
}
