using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo2 : MonoBehaviour
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

    public float tempoMovimentoX, tempoparado, tempoMovimentoZ;
    public float RedutorMovimento;

    public bool TomouDano = false;

    Collider[] MinhaPosicao;

    public GameObject Antidoto;

    public int Combo = 0;
    public float tempocombo;
    #endregion


    void Start()
    {
        inimrb = GetComponent<Rigidbody>();
        inimanim = GetComponent<Animator>();
       // tempoMovimento = 1; ;
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
        velocidadex = velocidademax;
        velocidadez = velocidademax;

        tempoparado -= redutor * Time.deltaTime;
        if (tempoparado <= 0 && !parado)
        {
            tempoparado = 1;
            parado = true;
        }

        if (tempoparado <= 0 && parado)
        {
            tempoparado = 1;
            parado = false;
        }

        if (parado)
        {
            velocidademax = 0;
            eixox = 0;
            eixoz = 0;
        }
        else
            velocidademax = 4;

        MinhaPosicao = Physics.OverlapSphere(transform.position, 10, 1 << LayerMask.NameToLayer("CamadaPlayer"));

        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));
        inimanim.SetBool("InNochao", nochao);

        if (olhad)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        //olhad = (alvo.position.x < transform.position.x) ? false : true;

        if (alvo.position.x < transform.position.x)
            olhad = false;
        else
            olhad = true;

        distanciaalvo = alvo.position - transform.position;
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
            if (MinhaPosicao.Length != 0)
            {
                if (TomouDano)
                {
                    inimanim.SetBool("InDano", true);
                    eixoz = 0;
                    eixox = 0;
                    tempo = 2;
                    tempocombo = 2;
                    if (Combo >= 4)
                        inimanim.SetBool("KO", true);

                }

                else
                {
                    if (tempocombo >= 0)
                        tempocombo -= Time.deltaTime;
                    else
                        Combo = 0;

                    inimanim.SetBool("InDano", false);
                    inimanim.SetBool("KO", false);
                    Combo = 0;
                    float Aceleracaox = eixox * velocidadex;
                    float Aceleracaoz = eixoz * velocidadez;

                    inimrb.velocity = new Vector3(Aceleracaox, inimrb.velocity.y, Aceleracaoz);

                    #region Movimento X
                    if (alvo.position.x < transform.position.x)
                    {
                        tempoMovimentoX -= redutor * Time.deltaTime;
                        if (tempoMovimentoX <= 0)
                        {
                            eixox = Random.Range(-1, -0.5f);
                            tempoMovimentoX = .1f;
                        }
                    }
                    else
                    {
                        tempoMovimentoX -= redutor * Time.deltaTime;
                        if (tempoMovimentoX <= 0)
                        {
                            eixox = Random.Range(1, 0.5f);
                            tempoMovimentoX = .1f;
                        }
                    }
                    #endregion

                    #region Movimento Z

                    if (alvo.position.z< transform.position.z)
                    {
                        tempoMovimentoZ -= redutor * Time.deltaTime;
                        if (tempoMovimentoZ <= 0)
                        {
                            eixoz = Random.Range(-4, -0.5f);
                            tempoMovimentoZ = Random.Range(0.5f, 2f);
                        }
                    }
                    else
                    {
                        tempoMovimentoZ -= redutor * Time.deltaTime;
                        if (tempoMovimentoZ <= 0)
                        {
                            eixoz = Random.Range(4, 0.5f);
                            tempoMovimentoZ = Random.Range(0.5f, 2f); 
                        }
                    }
                    #endregion

                    if (distanciaalvo.magnitude < 1.7f)
                    {
                        tempo -= redutor * Time.deltaTime;
                        if (tempo <= 1)
                        {
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
            inimanim.SetBool("InMorto", true);

        #region Limite em Z


        if (transform.position.z >= -1.5f && eixoz > 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, 0);

        if (transform.position.z >= -1.5f && eixoz < 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, inimrb.velocity.z);

        if (transform.position.z <= -9f && eixoz < 0)
            inimrb.velocity = new Vector3(inimrb.velocity.x, inimrb.velocity.y, 0);

        if (transform.position.z <= -9f && eixoz > 0)
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
            Combo++;
            Vida -= 10;
        }

        if (Encosta.gameObject.tag == "TagAtaquePl2")
        {
            SemVelocidade();

            TomouDano = true;
            Vida -= 25;
            inimanim.SetBool("KO", true);
        }

        if (Encosta.gameObject.tag == "TagAtaquePl3")
        {
            SemVelocidade();

            TomouDano = true;
            Vida -= 50;
            inimanim.SetBool("KO", true);
        }
    }
    void Impulso()
    {
        if (olhad)
            inimrb.AddForce(new Vector3(-10, 16, 0), ForceMode.Impulse);

        else
            inimrb.AddForce(new Vector3(10, 16, 0), ForceMode.Impulse);
    }

    void Destroi()
    {
        Destroy(gameObject);
    }

    public void IstanciarAntidoto()
    {
        int chances = Random.Range(1, 6);
        if (chances == 3 || chances == 5||chances==1)
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
}