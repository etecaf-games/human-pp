using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano : MonoBehaviour
{
    public SpriteRenderer SpriteJogador;

    float Clareando;
    float RedutorBrilho;

    float TempoDeDano;
    float RedutorDano;

    public bool TomouDano = false;

    // Start is called before the first frame update
    void Start()
    {
        Clareando = 1;
        RedutorBrilho = .05f;

        TempoDeDano = 1;
        RedutorDano = 0.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TomouDano)
        {
            TempoDeDano -= RedutorDano;
            if (TempoDeDano > 0)
            {
                Clareando -= RedutorBrilho;
                SpriteJogador.color = new Color(1f, 1f, 1f, Clareando);
                if (Clareando < 0.05f)
                {
                    Clareando = 1f;
                    SpriteJogador.color = new Color(1f, 1f, 1f, Clareando);
                }
            }
            else
            {
                TomouDano = false;
                TempoDeDano = 1;
            }
        }
        else
        {
            Clareando = 1f;
            SpriteJogador.color = new Color(1f, 1f, 1f, Clareando);
        }
    }

    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaqueIn")
            TomouDano = true;

        if (Encosta.gameObject.tag == "TagAtaquePl")
            TomouDano = true;
    }
}