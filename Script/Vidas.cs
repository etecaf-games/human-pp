//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
    public int vidasplayer;
    private Vidas vidas;
    //public Transform reviver;
    //public GameObject gameover;
    // Start is called before the first frame update
    void Awake()
    {
        if (vidas==null)
        {
            vidas = gameObject.GetComponent<Vidas>();
            
        }
        else
        {
            Debug.Log("jbj");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (vidasplayer<=0)
        {
            Destroy(gameObject);
        }
    }
}
