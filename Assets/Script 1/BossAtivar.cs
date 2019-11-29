using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossAtivar : MonoBehaviour
{
    public GameObject player, boss,canvas;
    public GameObject[] dialogo;
    public bool ativo = false;
    public int d = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ativo==true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (d<4)
                {
                    dialogo[d].SetActive(false);
                    d++;
                    dialogo[d].SetActive(true);
                   
                }
                else
                {
                    
                    canvas.SetActive(false);
                    player.GetComponent<Jogador>().enabled = true;
                    player.GetComponent<Jogador>().velocidademax = 13f;
                    boss.GetComponent<ScrBossRato>().enabled = true;
                }
          

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            ativo = true;
            dialogo[d].SetActive(true);
            canvas.SetActive(true);
            player.GetComponent<Jogador>().velocidademax = 0;
            player.GetComponent<Jogador>().enabled = false;
        }
    }
}
