//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Sombra : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player!=null)
        {
            float x = player.transform.position.x, z = player.transform.position.z;
            transform.position = new Vector3(x, transform.position.y, z);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
