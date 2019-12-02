using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo3 : MonoBehaviour
{
    #region Variaveis

    public int Vida;

    Rigidbody inimrb;
    Animator inimanim;
    public Transform checachao;
    Transform alvo;
    private Vector3 distanciaalvo;

    public float eixox;
    public float eixoz;
    public float tempo = 2;
    public float redutor = 0.5f;

    float velocidadex;
    float velocidadez;
    public float velocidademax = 3;

    bool nochao;
    public bool olhad = true;
    bool inimtamorto = false;
    bool parado;

    public SpriteRenderer SpriteJogador;

    float Clareando;
    float RedutorBrilho;
    float TempoDeDano;
    float RedutorDano;

    public float TempoMovimento;
    public float RedutorMovimento;

    public bool TomouDano = false;
    //public float tempocombo;
    // Collider[] MinhaPosicao;

    public GameObject Antidoto;
    public AudioSource[] audios;
    //public int Combo = 0;
    #endregion

    //public bool test;
    void Start()
    {
        inimrb = GetComponent<Rigidbody>();
        inimanim = GetComponent<Animator>();
        TempoMovimento = 1; ;
        RedutorMovimento = 1.5f;

        Clareando = 1;
        RedutorBrilho = .05f;
        TempoDeDano = 1;
        RedutorDano = 0.05f;
        alvo = FindObjectOfType<Jogador>().transform;
        velocidadex = velocidademax;
        velocidadez = velocidademax;
    }

    void Update()
    {
        //MinhaPosicao = Physics.OverlapSphere(transform.position, 20, 1 << LayerMask.NameToLayer("CamadaPlayer"));


        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));
        inimanim.SetBool("InNochao", nochao);
        if (inimtamorto == false)
        {
            if (olhad)
                transform.localScale = new Vector3(1.1f, 1.1f, 1);
            else
                transform.localScale = new Vector3(-1.1f, 1.1f, 1);
        }


        //olhad = (alvo.position.x < transform.position.x) ? false : true;
        if (alvo != null)
        {
            if (alvo.position.x < transform.position.x)
                olhad = false;
            else
                olhad = true;

            distanciaalvo = alvo.position - transform.position;
        }



    }


    void FixedUpdate()
    {
        if (Vida <= 0)
            inimtamorto = true;

        #region Efeito Dano

        if (TomouDano)
        {
            TempoDeDano -= RedutorDano;
            if (TempoDeDano > 0)
            {
                Clareando -= RedutorBrilho;
                SpriteJogador.color = new Color(1.5f, 1.5f, 1.5f, Clareando);
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

        #endregion

        else
        {
            Clareando = 1f;
            SpriteJogador.color = new Color(1f, 1f, 1f, Clareando);
        }
        if (eixox != 0 || eixoz != 0)
            inimanim.SetInteger("InMovendo", 1);

        if (eixox == 0 && eixoz == 0)
            inimanim.SetInteger("InMovendo", 0);

        if (!inimtamorto)
        {
            if (alvo != null)
            {
                if (TomouDano)
                {
                    inimanim.SetBool("InDano", true);
                    eixoz = 0;
                    eixox = 0;
                    //SemVelocidade();
                    //tempocombo = 2;
                    FindObjectOfType<vidainimigo>().inimigoVida(Vida);
                    tempo = 2;
                    //if (Combo > 3)
                    //{
                    //Impulso();
                    //    inimanim.SetBool("KO", true);
                    //    //inimanim.SetTrigger("KO2");
                    //    tempocombo = 0;
                    //    Combo = 0;
                    //}


                }

                else
                {
                    //if (tempocombo >= 0)
                    //    tempocombo -= Time.deltaTime;
                    //else
                    //    Combo = 0;

                    inimanim.SetBool("InDano", false);
                    //inimanim.SetBool("KO", false);
                    float Aceleracaox = eixox * velocidadex;
                    float Aceleracaoz = eixoz * velocidadez;
                    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -14f, -1.5f));
                    inimrb.velocity = new Vector3(Aceleracaox, inimrb.velocity.y, Aceleracaoz);

                    if (alvo.position.x < transform.position.x)
                    {
                        TempoMovimento -= RedutorMovimento * Time.deltaTime;
                        if (TempoMovimento <= 0)
                        {
                            eixox = Random.Range(-1, -0.1f);
                            eixoz = Random.Range(-.5f, .5f);
                            TempoMovimento = 1;
                        }
                    }
                    else
                    {
                        TempoMovimento -= RedutorMovimento * Time.deltaTime;
                        if (TempoMovimento <= 0)
                        {
                            eixox = Random.Range(1, 0.1f);
                            eixoz = Random.Range(-.5f, .5f);
                            TempoMovimento = 1;
                        }
                    }

                    if (distanciaalvo.magnitude < 2.3f)
                    {
                        tempo -= redutor * Time.deltaTime;
                        if (tempo <= 1)
                        {
                            audios[0].Play();
                            inimanim.SetTrigger("InAtacar");
                            tempo = Random.Range(1.5f, 1.1f);
                        }
                        eixoz = 0;
                        eixox = 0;
                    }



                }
            }
            else
            {
                eixoz = 0;
                eixox = 0;
            }
        }
        else
        {
            FindObjectOfType<vidainimigo>().inimigoVida(Vida);
            inimanim.SetBool("InMorto", true);
        }


        #region Limite em Z


        if (transform.position.z >= -1.5f && eixoz > 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, 0);

        if (transform.position.z >= -1.5f && eixoz < 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, inimrb.velocity.z);

        if (transform.position.z <= -12f && eixoz < 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, 0);

        if (transform.position.z <= -12f && eixoz > 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, inimrb.velocity.z);


        //if (transform.position.x <= Camera.main.transform.position.x && eixox < 0)
        //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

        //if (transform.position.x >= Camera.main.transform.position.x && eixox > 0)
        //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        #endregion
    }

    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaquePl")
        {
            TomouDano = true;
            //Combo++;
            Vida -= 10;
            audios[1].Play();

        }

        if (Encosta.gameObject.tag == "TagAtaquePlGiro")
        {
            Vida -= 10;
            audios[1].Play();
            inimanim.SetBool("KO", true);
            Impulso();
        }

        if (Encosta.gameObject.tag == "TagAtaquePl2")
        {
            SemVelocidade();
            audios[1].Play();
            TomouDano = true;
            Vida -= 25;
            //Combo += 4;
            inimanim.SetBool("KO", true);
        }

        if (Encosta.gameObject.tag == "TagAtaquePl3")
        {
            SemVelocidade();
            audios[1].Play();
            TomouDano = true;
            Vida -= 50;
            //Combo += 4;
            inimanim.SetBool("KO", true);
        }
    }

    void Impulso()
    {
        if (olhad)
            inimrb.AddForce(new Vector3(-19, 5, 0), ForceMode.Impulse);

        else
            inimrb.AddForce(new Vector3(19, 5, 0), ForceMode.Impulse);
    }


    void Destroi()
    {
        Destroy(gameObject);
    }

    public void IstanciarAntidoto()
    {
        int chances = Random.Range(1, 6);
        if (chances == 3 || chances == 5 || chances == 1)
        {
            Debug.Log("Acertou");
            Instantiate(Antidoto, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Errouu");
        }

    }
    void SemVelocidade()
    {
        velocidadex = 0;
        velocidadez = 0;
    }

    void ResetaVelocidade()
    {
        velocidadex = velocidademax;
        velocidadez = velocidademax;

    }
    void SomMorte()
    {
        audios[2].Play();
    }
    void leventando()
    {
        inimanim.SetBool("KO", false);
    }
}
