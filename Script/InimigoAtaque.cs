//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class InimigoAtaque : MonoBehaviour
{ 
 public GameObject Agente, Player;

// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{

}

void OnTriggerEnter(Collider Encosta)
{
    if (Encosta.gameObject.tag == "Player")
    {
           
            if (Player.GetComponent<Jogador>().Combo>3)
            {
               

                if (Agente.GetComponent<Inimigo>().olhad == true)
                {
                   // Player.GetComponent<Rigidbody>().AddForce(new Vector3(22, 0, 0), ForceMode.Impulse);
                   FindObjectOfType<Jogador>().rb.AddForce(new Vector3(22, 0, 0), ForceMode.Impulse);
                   
                }
                else
                {
                  FindObjectOfType<Jogador>().rb.AddForce(new Vector3(-22, 0, 0), ForceMode.Impulse);
                   // Player.GetComponent<Rigidbody>().AddForce(new Vector3(-22, 0, 0), ForceMode.Impulse);
           
                }

            }
      
    }
}
}