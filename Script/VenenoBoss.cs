using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenenoBoss : MonoBehaviour
{
    Animator ani;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();

        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider Encosta)
    {

        body.useGravity = false;
        
        if (Encosta.gameObject.tag == "Player")
        {
            ani.SetInteger("quem",1);
        }

        if (Encosta.gameObject.tag == "Chão")
        {
            ani.SetInteger("quem", 2);
        }
 
    }

    void Destroi()
    {
        Destroy(gameObject);
    }
  
}
