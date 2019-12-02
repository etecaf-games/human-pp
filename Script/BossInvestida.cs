//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BossInvestida : MonoBehaviour
{
    public GameObject Boss, Player;

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
            Boss.GetComponent<Animator>().SetBool("BossInvestida",false);
            Boss.GetComponent<ScrBossRato>().investida = false;
            Boss.GetComponent<ScrBossRato>().tempoInvestida = 5f;
            if (Boss.GetComponent<ScrBossRato>().olhad==true)
            {
               Player.GetComponent<Rigidbody>().AddForce(new Vector3(25, 0, 0), ForceMode.Impulse);
            }
            else
            {
                Player.GetComponent<Rigidbody>().AddForce(new Vector3(-25, 0, 0), ForceMode.Impulse);
            }
        }
    }
}
