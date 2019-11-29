//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AterteEnter : MonoBehaviour
{
    public GameObject AperteEnter;
    public GameObject Painel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AperteEnter.SetActive(false);
            Painel.SetActive(true);          
        }
        
    }
}
