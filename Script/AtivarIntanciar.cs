using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarIntanciar : MonoBehaviour
{
    
    public GameObject player;
    BoxCollider box;
    IntaciadorInimigo it;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        it = GetComponent<IntaciadorInimigo>();
        it.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            it.enabled = true;
           
            box.enabled = false;
            gameObject.tag = "intaciar";
        }
    }
}
