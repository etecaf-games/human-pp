//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class BossVeneno : MonoBehaviour
{
    public GameObject boss;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            boss.GetComponent<ScrBossRato>().namira = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            boss.GetComponent<ScrBossRato>().namira = false;
        }
    }
}
