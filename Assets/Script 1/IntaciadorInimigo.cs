using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntaciadorInimigo : MonoBehaviour
{
    public GameObject inimigo;
  //  public GameObject player;
    public int numinimigos,Inimigosrestantes;
    public GameObject poartcamera;
    public Transform intanciar1, intanciar2;
    public bool intanciar = true;
   // BoxCollider box;
    public float espera=2;
    // Start is called before the first frame update
    void Start()
    {
       // box = GetComponent<BoxCollider>();
        //imimigosrestantes = numinimigos;
    }

    // Update is called once per frame
    void Update()
    {
        Inimigosrestantes = FindObjectsOfType<Inimigo>().Length;
        if (intanciar == true )
        {
         
            poartcamera.GetComponent<Camera1>().enabled = false;
            espera -= Time.deltaTime;
            if (espera<=0)
            {
                int rannum = Random.Range(0, 2);
                if (rannum==0)
                {
                    espera = 2;
                    numinimigos--;
                    Instantiate(inimigo, intanciar1.position, Quaternion.identity);
                }
                else
                {
                    numinimigos--;
                    espera = 2;
                    Instantiate(inimigo, intanciar2.position, Quaternion.identity);
                }
                if (numinimigos==0)
                {
                    intanciar = false;
                }
                
            }
        }
        if (Inimigosrestantes==0&numinimigos==0)
        {
          
            poartcamera.GetComponent<Camera1>().enabled = true;
            Destroy(this.gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag=="Player")
    //    {
    //        imimigosmortos = numinimigos;
    //        box.enabled = false;
    //        intanciar = true;
    //        poartcamera.GetComponent<Camera1>().enabled = false;
    //    }
    //}
}
