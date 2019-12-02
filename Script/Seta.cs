//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Seta : MonoBehaviour
{
    public GameObject seta, poartcamera, player;
    public float tempo = 0.1f;
    public bool ativo = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ativo == true)
        {
            tempo -= Time.deltaTime;
            if (poartcamera.transform.position.x < player.transform.position.x & tempo <= 0)
            {
                float a = poartcamera.transform.position.x + 0.1f;
                poartcamera.transform.position = new Vector3(a, poartcamera.transform.position.y, poartcamera.transform.position.z);
              
                tempo += 0.01f;
                if (poartcamera.transform.position.x >= player.transform.position.x)
                {
                    poartcamera.GetComponent<Camera1>().enabled = true;
                 
                    seta.SetActive(false);
                }
            }
         
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            

            ativo = true;

        }

    }

 
  
}

