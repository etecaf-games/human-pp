using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrBossRato : MonoBehaviour
{
    #region Variaveis
    public int Vida;
    public Rigidbody BossRb;
    public Animator BossAnim;
    public Transform checachao,localtiro;
    public Transform RobertPosicao;

    Vector3 distanciaalvo;

    public float EixoX;
    public float EixoZ;

    public float tempoInvestida = 3;
    float tempoMudaCena = 2; // tempo para mudar de cena
    float tempoParado = 1;
    float tempoMovimentoX = .1f;
    float tempoMovimentoZ = .8f;
    float tempoAtaque = .5f;
    float redutor = 0.5f;

    float velocidadex;
    float velocidadez;
    public float velocidademax;


    public bool MudaFase = false; // bool para ativar animação do Robert
    public SpriteRenderer SpriteBoss;
    bool Parado;
    bool nochao;
    public bool olhad = true;
    bool inimtamorto = false;
    public bool TomouDano = false;

    float Clareando;
    float RedutorBrilho;
    float TempoDeDano;
    float RedutorDano;
    public AudioSource[] audios;

    public GameObject veneno,Antidoto;
    public float tempoveneno=15f,tempoantidoto=20f, tempocombo;
    public Sprite retrato;
  //  public Transform boca;
    public bool namira;
    public float velocidainvestida;
    #endregion

    public bool investida = false;

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

 

        if (tempoParado <= 0 && !Parado && investida==false)
        {
            tempoParado = 1;
            Parado = true;
            if (namira & tempoInvestida >= 0 & tempoveneno <= 0)
                BossAnim.SetTrigger("BossVeneno");

        }
        if (namira & tempoInvestida >= 0 & tempoveneno <= 0)
        {
            BossAnim.SetTrigger("BossVeneno");
            tempoveneno += 10;
        }

        if (RobertPosicao!=null)
        {
            if (olhad)
                transform.localScale = new Vector3(1.4f, 1.4f, 1);
            else
                transform.localScale = new Vector3(-1.4f, 1.4f, 1);

            //olhad = (alvo.position.x < transform.position.x) ? false : true;

            if (RobertPosicao.position.x < transform.position.x)
                olhad = false;
            else
                olhad = true;

            distanciaalvo = RobertPosicao.position - transform.position;
        }

        if (tempoantidoto>=0)
            tempoantidoto -= Time.deltaTime;

        else
        {
           GameObject anti= Instantiate(Antidoto, transform.position, Quaternion.identity);
           if (olhad)
               anti.GetComponent<Rigidbody>().AddForce(new Vector3(20, 8, 0), ForceMode.Impulse);

           else
               anti.GetComponent<Rigidbody>().AddForce(new Vector3(-20, 8, 0), ForceMode.Impulse);
           
            tempoantidoto += 5;
        }

        if (tempoveneno>=0)
            tempoveneno -= Time.deltaTime; 

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
                tempocombo = 2;
                FindObjectOfType<vidainimigo>().rosto.sprite = retrato;
                FindObjectOfType<vidainimigo>().inimigoVida(Vida);

            
                audios[0].Play();
                BossAnim.SetBool("BossDano", true);
                Clareando -= RedutorBrilho;
                SpriteBoss.color = new Color(1.5f, 1.5f, 1.5f, Clareando);
                if (Clareando < 0.05f)
                {
                    Clareando = 1f;
                    SpriteBoss.color = new Color(1f, 1f, 1f, Clareando);
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
            BossAnim.SetBool("BossKO", false);
            BossAnim.SetBool("BossDano", false);
            Clareando = 1f;
            SpriteBoss.color = new Color(1f, 1f, 1f, Clareando);

            if (!inimtamorto)
            {

                float Aceleracaox = EixoX * velocidadex;
                float Aceleracaoz = EixoZ * velocidadez;

                BossRb.velocity = new Vector3(Aceleracaox, BossRb.velocity.y, Aceleracaoz);

                if (EixoX != 0 || EixoZ != 0)
                    BossAnim.SetInteger("BossMovendo", 1);
                if (EixoX == 0 && EixoZ == 0)
                    BossAnim.SetInteger("BossMovendo", 0);

                if (distanciaalvo.magnitude < 2.5 && investida==false)
                {
                    #region Ataque
                    tempoInvestida = 5;
                    tempoAtaque -= redutor * Time.deltaTime;
                    if (tempoAtaque <= 1)
                    {
                        Somataque();
                        BossAnim.SetTrigger("BossAtacar");
                        tempoAtaque = Random.Range(2, 1.5f);
                    }
                    if (namira  & tempoveneno <= 0)
                    {
                        BossAnim.SetTrigger("BossVeneno");
                        tempoveneno += 10;
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
                        investida = true;
                        Parado = false;
                        BossAnim.SetBool("BossInvestida", true);
                    }

                    if (RobertPosicao != null && investida==true)
                    {

                        velocidademax = velocidainvestida;
                        if (RobertPosicao.position.x < transform.position.x)
                            EixoX = -1;
                        else
                            EixoX = 1;
                        if (RobertPosicao.position.z < transform.position.z)
                            EixoZ = -1;
                        else
                            EixoZ = 1;
                    }
                    if (RobertPosicao != null && investida == false)
                    {

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
                            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, -10f, -1.5f));
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
            }
            else
            {
                BossAnim.SetBool("BossMorto", true);
                MudaFase = true;

                tempoMudaCena -= redutor * Time.deltaTime;
                if (tempoMudaCena <= 0)
                {
                    SceneManager.LoadScene("Cena_Transição_1");
                }
            }
        }
    }

    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaquePl" & inimtamorto ==false)
        {
            TomouDano = true;
            SomDano();
            Vida -= 10;
        }

        if (Encosta.gameObject.tag == "TagAtaquePlGiro")
        {
            Vida -= 10;
            audios[1].Play();
            BossAnim.SetBool("KO", true);
            if (olhad)
                ImpulsoD();
            else
                ImpulsoE();
        }

        if (Encosta.gameObject.tag == "TagAtaquePl2" & inimtamorto == false)
        {
            TomouDano = true;
            SomDano();
            Vida -= 20;
        }

        if (Encosta.gameObject.tag == "TagAtaquePl3" & inimtamorto == false)
        {
            SomDano();
            TomouDano = true;
            Vida -= 30;
            if (olhad)
                ImpulsoD();
            else
                ImpulsoE();
        }
    }

    void ImpulsoD()
    {
        BossRb.AddForce(new Vector3(-30, 10, 0), ForceMode.Impulse);
    }
    void ImpulsoE()
    {
        BossRb.AddForce(new Vector3(30, 10, 0), ForceMode.Impulse);
    }
    public void Somataque()
    {
        audios[1].Play();
    }
    public void SomDano()
    {
        audios[0].Play();
    }
    public void SomMorte()
    {
        audios[2].Play();
    }
    public void Veneno()
    {
        GameObject venon = Instantiate(veneno, localtiro.position, Quaternion.identity);
        if (olhad)
        {
            venon.GetComponent<Rigidbody>().AddForce(new Vector3(15, 4, 0), ForceMode.Impulse);
        }
        else
        {
            venon.GetComponent<Rigidbody>().AddForce(new Vector3(-15, 4, 0), ForceMode.Impulse);
        }

        tempoveneno += 5;
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

    void leventando()
    {
        BossAnim.SetBool("KO", false);
    }

    void semdano() { BossAnim.SetBool("BossDano", false); }

    void Fimivsetida() { BossAnim.SetBool("BossInvestida", false); }
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

