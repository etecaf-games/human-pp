using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrZoom : MonoBehaviour
{
    public float ContagemCena = 9;
    public float Contagem = 6;
    float Redutor = 1;

    float LimiteScala = 3.12f;
    float Posx = 11f;
    float Posy = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ContagemCena -= Redutor * Time.deltaTime;
        Contagem -= Redutor * Time.deltaTime;

        if (Contagem <= 0)
        {
            if (transform.position.x <= Posx)
                transform.position = new Vector3(transform.position.x + .08f, transform.position.y, transform.position.z);

            if (transform.position.y <= Posy)
                transform.position = new Vector3(transform.position.x, transform.position.y + .03f, transform.position.z); 


            if (transform.localScale.x <= LimiteScala)
                transform.localScale = new Vector3(transform.localScale.x + .0155f, transform.localScale.y + .0155f, 0);
        }

        if (ContagemCena <= 0)
        {
            SceneManager.LoadScene("Fase_2");
        }
    }
}