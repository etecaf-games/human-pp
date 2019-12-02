using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSoco : MonoBehaviour
{
    public bool acertou = false;
    public AudioSource[] soco;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (acertou==false)
        {
            soco[0].Play();
        }
        else
        {
            soco[1].Play();
        }
    }
    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag=="TagInimigo")
        {
            acertou = true; 
        }
    
    }
}
