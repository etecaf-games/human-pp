using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrBossRato : MonoBehaviour
{
    #region Variaveis
    public int Vida;
    public Rigidbody BossRb;
    public Animator BossAnim;
    public Transform checachao;
    public Transform RobertPosicao;

    Vector3 distanciaalvo;

    public float EixoX;
    public float EixoZ;

    public float tempoInvestida = 3;
    float tempoParado = 1;
    float tempoMovimentoX = .1f;
    float tempoMovimentoZ = .8f;
    float tempoAtaque = .5f;
    float redutor = 0.5f;

    float velocidadex;
    float velocidadez;
    public float velocidademax;
    public SpriteRenderer SpriteBoss;
    bool Parado;
    bool nochao;
    bool olhad = true;
    bool inimtamorto = false;
    public bool TomouDano = false;

    float Clareando;
    float RedutorBrilho;
    float TempoDeDano;
    float RedutorDano;
    public AudioSource[] audios;



    #endregion

    // Use this for initialization
    void Start()
    {
        Parado = false;
        velocidadex = velocidademax;
        velocidadez = velocidademax;
        Clareando = 1;
        RedutorBrilho = .05f;
        TempoDeDano = 1;
        RedutorDano = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        velocidadex = velocidademax;
        velocidadez = velocidademax;

        tempoParado -= redutor * Time.deltaTime;
        if (tempoParado <= 0 && !Parado)
        {
            tempoParado = 1;
            Parado = true;
        }

        if (tempoParado <= 0 && Parado)
        {
            tempoParado = 1;
            Parado = false;
        }

        if (Parado)
        {
            velocidademax = 0;
            EixoX = 0;
            EixoZ = 0;
        }
        else
            velocidademax = 8;

        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));
        BossAnim.SetBool("BossNochao", nochao);

        if (olhad)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);

        //olhad = (alvo.position.x < transform.position.x) ? false : true;

        if (RobertPosicao.position.x < transform.position.x)
            olhad = false;
        else
            olhad = true;

        distanciaalvo = RobertPosicao.position - transform.position;
    }


    void FixedUpdate()
    {
        if (Vida <= 0)
            inimtamorto = true;

        if (TomouDano)
        {
            TempoDeDano -= RedutorDano;
            if (TempoDeDano > 0)
            {
                audios[0].Play();
                //BossAnim.SetTrigger("BossDano");
                Clareando -= RedutorBrilho;
                SpriteBoss.color = new Color(1.5f, 1.5f, 1.5f, Clareando);
                if (Clareando < 0.05f)
                {
                    Clareando = 1f;
                    SpriteBoss.color = new Color(0f, 0f, 0f, Clareando);
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
            SpriteBoss.color = new Color(0f, 0f, 0f, Clareando);
            if (!inimtamorto)
            {

                float Aceleracaox = EixoX * velocidadex;
                float Aceleracaoz = EixoZ * velocidadez;

                BossRb.velocity = new Vector3(Aceleracaox, BossRb.velocity.y, Aceleracaoz);

                if (EixoX != 0 || EixoZ != 0)
                    BossAnim.SetInteger("BossMovendo", 1);
                if (EixoX == 0 && EixoZ == 0)
                    BossAnim.SetInteger("BossMovendo", 0);

                if (distanciaalvo.magnitude < 2)
                {
                    #region Ataque
                    tempoInvestida = 3;
                    tempoAtaque -= redutor * Time.deltaTime;
                    if (tempoAtaque <= 1)
                    {

                        BossAnim.SetTrigger("BossAtacar");
                        tempoAtaque = Random.Range(2, 1.5f);
                    }

                    EixoZ = 0;
                    EixoX = 0;
                    #endregion
                }

                else
                {
                    tempoInvestida -= redutor * Time.deltaTime;
                    if (tempoInvestida <= 0)
                    {
                        Parado = false;
                        BossAnim.SetTrigger("BossInvestida");
                        velocidademax = 20;
                        if (RobertPosicao.position.x < transform.position.x)
                            EixoX = -1;
                        else
                            EixoX = 1;
                        if (RobertPosicao.position.z < transform.position.z)
                            EixoZ = -1;
                        else
                            EixoZ = 1;
                    }

                    #region Movimento X
                    if (RobertPosicao.position.x < transform.position.x)
                    {
                        tempoMovimentoX -= redutor * Time.deltaTime;
                        if (tempoMovimentoX <= 0)
                        {
                            EixoX = Random.Range(-1, -0.5f);
                            tempoMovimentoX = .1f;
                        }
                    }
                    else
                    {
                        tempoMovimentoX -= redutor * Time.deltaTime;
                        if (tempoMovimentoX <= 0)
                        {
                            EixoX = Random.Range(1, 0.5f);
                            tempoMovimentoX = .1f;
                        }
                    }
                    #endregion

                    #region Movimento Z

                    if (RobertPosicao.position.z < transform.position.z || RobertPosicao.position.z > transform.position.z)
                    {
                        tempoMovimentoZ -= redutor * Time.deltaTime;
                        if (tempoMovimentoZ <= 0)
                        {
                            EixoZ = Random.Range(0.5f, -0.5f);
                            tempoMovimentoZ = .8f;
                        }
                    }
                    #endregion
                }
            }
            else
            {
                audios[3].Play();
                BossAnim.SetBool("BossMorto", true);
            }
        }


    }
    void Destroi()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaquePl")
        {
            TomouDano = true;

            Vida -= 10;
        }

        if (Encosta.gameObject.tag == "TagAtaquePl2")
        {
            TomouDano = true;
            Vida -= 25;

        }

        if (Encosta.gameObject.tag == "TagAtaquePl3")
        {
            TomouDano = true;
            Vida -= 80;

        }
    }

    void Impulso()
    {
        if (olhad)
            BossRb.AddForce(new Vector3(-10, 16, 0), ForceMode.Impulse);

        else
            BossRb.AddForce(new Vector3(10, 16, 0), ForceMode.Impulse);
    }
    public void Somataque()
    {
        audios[1].Play();
    }

}




//if (distanciaalvo.magnitude > 2)
//            {
//                tempoInvestida -= redutor * Time.deltaTime;
//                if (tempoInvestida <= 0)
//                {
//                    BossAnim.SetTrigger("BossInvestida");
//                    velocidademax = 15;
//                    if (RobertPosicao.position.x < transform.position.x)
//                        eixox = -1;
//                    else
//                        eixox = 1;
//                    if (RobertPosicao.position.z < transform.position.z)
//                        eixoz = -1;
//                    else
//                        eixoz = 1;

//                    tempoInvestida = 3;

//                    if (distanciaalvo.magnitude > 2)
//                    {
//                        eixox = 0;
//                        eixoz = 0;
//                    }
//                }

