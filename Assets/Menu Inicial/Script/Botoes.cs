//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botoes : MonoBehaviour
{
    public GameObject fadeouti;
    public void InicioJogo()
    {
        fadeouti.SetActive(true);
    }
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void fadeout()
    {
        SceneManager.LoadScene("Fase_1");
    }
}
