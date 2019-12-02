//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrVideo : MonoBehaviour
{
    public GameObject Fade;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame;
    void Update()
    {
        
       
       
        if (Input.anyKeyDown)
        {
            Fade.SetActive(true);
            
        }

    }

    void FadeOut()
    {
        SceneManager.LoadScene("Tela_Inicial");
    }

}