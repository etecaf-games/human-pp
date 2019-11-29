using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Pause : MonoBehaviour
{
    public GameObject Painel, Painel1;
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            Player.GetComponent<Jogador>().enabled = true;
            Painel.SetActive(true);
            Time.timeScale = 0f;
        }
     
        
    }
    public void Resume()
    {
        Painel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MenuIncial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicial");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Historia()
    {
        Painel1.SetActive(true);
        Painel.SetActive(false);
      
    }
    public void Voltar()
    {
        Painel1.SetActive(false);
        Painel.SetActive(true);
        
    }
}
