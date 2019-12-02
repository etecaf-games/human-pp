//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
        #region Variaveis

    public ScrBossRato scrRato;
    public int Vida;
    public float Toxina;
    public int MP;
    public  int Combo;

    float TempoToxina;
    float Redutor;

    private float velocidadex;
    private float velocidadez;
    private float velocidade;
    public float velocidademax = 9;
    public float forcapulo = 300;

    public Slider[] Barras;
   // Inimigo inimigo;
    public GameObject cam;
    public Animator animcam;
    Animator anim;
    public Rigidbody rb;
    public Transform checachao;

    //bool treme = false;
    bool nochao;
    bool tamorto = false;
    bool olhad = true;
    bool pulo = false;
    public bool Atacando = false;
    public bool Acertou = false;

    public SpriteRenderer SpriteJogador;

    float Clareando;
    float RedutorBrilho;

    float TempoDeDano;
    float RedutorDano;

    public bool TomouDano = false;
    public AudioSource[] audios;
    public GameObject GameOver;

    public float TempoVida = 1;
    public float Tempotoxina = 1;
    public float tempocombo;

    public Image rosto;
    public Sprite[] rostos;

    #endregion

    void Start () 
    {
        
        rb = GetComponent<Rigidbody>();
        // inimigo = GetComponent<Inimigo>();
        anim = GetComponent<Animator>();

        //  Vida = 100;
        Toxina = 0;
        MP = 0; 

        Redutor = 2;

        Clareando = 1;
        RedutorBrilho = .05f;

        TempoDeDano = 1;
        RedutorDano = 0.05f;
	}
	
	void Update () 
    {
        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));

        anim.SetBool("NoChao", nochao);
        anim.SetFloat("VelocidadeY", rb.velocity.y);

        if (Input.GetButton("Jump") && nochao)
            pulo = true;

        velocidadex = velocidademax;
        velocidadez = velocidademax;
	}

    void FixedUpdate()
    {
        if (Vida <= 0 || Toxina >= 100)
            tamorto = true;

        #region Limites Barras Interativas

        if (MP >= 100) 
            MP = 100;
        if (MP <= 0)
            MP = 0;

        if (Vida >= 100) //mnoijdsfuhngun
            Vida = 100;
        if (Vida <= 0)
            Vida = 0;

     
        if (Toxina <= 0)
            Toxina= 0;

        //if (Barras[0].value >= Vida)
        //    Barras[0].value = Vida;

        //if (Barras[0].value <= Barras[0].minValue)
        //    Barras[0].value = Barras[0].minValue;

        //if (Barras[1].value >= Toxina)
        //    Barras[1].value = Toxina;

        //if (Barras[1].value <= Barras[1].minValue)
        //    Barras[1].value = Barras[1].minValue;

        //if (Barras[2].value >= MP)
        //    Barras[2].value = MP;

        //if (Barras[2].value <= Barras[2].minValue)
        //    Barras[2].value = Barras[2].minValue;

        if (Vida>75)
        {
            rosto.sprite = rostos[0];
        }
        if (Vida<75&&Vida>50)
        {
            rosto.sprite = rostos[1];
        }
        if (Vida < 50 && Vida > 25)
        {
            rosto.sprite = rostos[2];
        }
        if (Vida < 25  && Vida > 0)
        {
            rosto.sprite = rostos[3];
        }
        if (Vida == 0)
        {
            rosto.sprite = rostos[4];
        }

        #endregion

        #region Efeito de Dano

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
                TempoDeDano = 1.5f;
            }
        }
        else
        {
            Clareando = 1f;
            SpriteJogador.color = new Color(1f, 1f, 1f, Clareando);
        }

#endregion
        if (scrRato.MudaFase)
            anim.SetTrigger("Comemora");

        if (!tamorto)
        {

            if (TomouDano)
            {
                tempocombo = 2;
              
                anim.SetBool("Dano", true);
                if (Combo >= 4)
                {
                    anim.SetBool("KO", true);
                    tempocombo = 0;
                    // Impulso();
                    // Combo = 0;
                }   
            }

            else
            {
                if (tempocombo >= 0)
                    tempocombo -= Time.deltaTime;
                else
                    Combo = 0;

                anim.SetBool("Dano", false);
                anim.SetBool("KO", false);

                Barras[0].value = Vida;
                Barras[1].value = MP;
                Barras[2].value = Toxina;
                if (Vida<100)
                {
                    TempoVida -= Time.deltaTime;
                    if (TempoVida <= 0 )
                    {
                        Vida++;
                        TempoVida += 1;
                    }
                }
                if (Toxina <=100)
                {
                    Tempotoxina -= Time.deltaTime;
                    if (Tempotoxina <= 0)
                    {
                        Toxina++;
                        Tempotoxina += 1;
                    }
                }
                //Barras[3].value = Toxina;
                //Toxina += Redutor * Time.deltaTime;    

                #region Movimento

                float eixox = Input.GetAxisRaw("Horizontal");
                float eixoz = Input.GetAxisRaw("Vertical");
                float limiteXmin = Camera.main.ViewportToWorldPoint(new Vector3(-4.8f, 0, 5)).x ;
                float limiteXmax = Camera.main.ViewportToWorldPoint(new Vector3(5.5f, 0, 5)).x ;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteXmin, limiteXmax), transform.position.y, transform.position.z);

                if (Atacando == false && rb.velocity.x != 0 || Atacando == false && rb.velocity.z != 0)
                {
                    rb.velocity = new Vector3(eixox * velocidadex, rb.velocity.y, eixoz * velocidadez);

                    if (eixox > 0 && !olhad)
                        Flip();
                    else if (eixox < 0 && olhad)
                        Flip();
                }


                if (pulo)
                {
                    rb.AddForce(Vector3.up * forcapulo);
                    pulo = false;
                }
                #endregion

                #region Limite em Z
             
                if (transform.position.z >= -1.5f && eixoz > 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

                if (transform.position.z >= -1.5f && eixoz < 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);

                if (transform.position.z <= -10f && eixoz < 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

                if (transform.position.z <= -10f && eixoz > 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);


                //if (transform.position.x <= Camera.main.transform.position.x && eixox < 0)
                //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                //if (transform.position.x >= Camera.main.transform.position.x && eixox > 0)
                //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                #endregion

                #region Animação

                if (rb.velocity.x == 0 && rb.velocity.z == 0)
                    anim.SetInteger("Movimento", 0);

                if (rb.velocity.x != 0 || rb.velocity.z != 0 )
                    anim.SetInteger("Movimento", 1);

                //if (rb.velocity.x != 0 && !Corre|| rb.velocity.z != 0 && !Corre)
                //    anim.SetInteger("Movimento", 1);

                //if (rb.velocity.x != 0 && Corre|| rb.velocity.z != 0 && Corre) // animação de correr
                //{

                //}
                if (Atacando)
                    pulo = false;
                if (Atacando)
                {
                    if(nochao)
                    velocidademax = 0;
                }
                else
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftShift) || Input.GetMouseButton(1))
                    {
                        anim.SetBool("Corre", true);
                        velocidademax = 18;
                    }
                    else
                        velocidademax = 9;

                if (Input.GetButtonDown("Fire1") && !Atacando && !nochao)
                {
                    anim.SetTrigger("Atacar");
                    Atacando = true;
                }

                if (rb.velocity.x == 0 && rb.velocity.z == 0 && nochao)
                {
                    if (Input.GetButton("Fire1") && !Atacando)
                    {
                        anim.SetTrigger("Atacar");
                        Atacando = true;
                        Acertou = true;
                    }

                    if (Input.GetButtonDown("Fire2") && MP > 25 && !Atacando)
                    {
                        anim.SetTrigger("Skill1");
                        Atacando = true;
                        eixox = 0;
                        audios[6].Play();
                    }

                    if (Input.GetButtonDown("Fire3") && MP > 80 && !Atacando)
                    {
                        anim.SetTrigger("Skill2");
                        Atacando = true;
                        audios[3].Play();
                    }
                } 
            }
        }
            #endregion
        else
        {
            GameOver.SetActive(true);
            audios[2].Play();
            anim.SetBool("Morto", true);
        }
    }

        #region Voids
    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaqueIn")
        {
            Combo++;
            TomouDano = true;
            Vida -= 5;
            MP += 10;
            audios[1].Play();
        }
        if (Encosta.gameObject.tag == "TagAtaqueInBoss")
        {
            //if (TomouDano)
            //    Combo++;
            TomouDano = true;
            Vida -= 15;
            MP += 10;
            audios[1].Play();
        }
        if (Encosta.gameObject.tag == "TagAtaqueInBoss2")
        {
        //    if (TomouDano)
        //        Combo = 4;
            anim.SetBool("KO", true);
            TomouDano = true;
            Vida -= 30;
            MP += 10;
            //Impulso();
            audios[1].Play();

        }
            if (Encosta.gameObject.tag == "TagInimigo" && Acertou)
            MP += 10;

            if (Encosta.gameObject.tag == "Antidoto")
            {
                audios[7].Play();
                Toxina -= 50;
                Destroy(Encosta.gameObject);
            }

            if (Encosta.gameObject.tag == "Veneno")
            {
            //audios[7].Play();
            TomouDano = true;
            Vida -= 25;
                Toxina += 15;   
            }
    }
    void Flip()
    {
        olhad = !olhad;
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    void SemVelocidade()
    {
        velocidademax = 0; 
    }
    void ResetaVelocidade()
    {
        velocidademax = 9;
    }
    void FimAtaque()
    {
        Acertou = false;
        Atacando = false;
    }
    void TremeCam()
    {
        animcam.SetBool("Vibra", true);
    }
    void ParaCam()
    {
        animcam.SetBool("Vibra", false);
    }
    void Impulso()
    {
        if (olhad)
        rb.AddForce(new Vector3(-18, 0, 0), ForceMode.Impulse);

        else
        rb.AddForce(new Vector3(18, 0, 0), ForceMode.Impulse);
    }
    void Destroi()
    {
        Destroy(gameObject);
    }
    void MPSkill1()
    {
        MP -= 25;
    }
    void MPSkill2()
    {
        MP -= 80;
    }
    void GolpeSom()
    {
        audios[0].Play();
    }
    void Voadorasom()
    {
        audios[4].Play();
    }
    void Quedasom()
    {
        audios[5].Play();
    }
    void mortesom()
    {
        audios[2].Play();
    }
    //void Combo0()
    //{
    //    Combo = 0;
    //}
    
#endregion
}
