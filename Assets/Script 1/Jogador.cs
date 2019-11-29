using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    #region Variaveis

    public int Vida;
    public float Toxina;
    public int MP;
    int Combo = 0;

    float TempoToxina;
    float Redutor;

    private float velocidadex;
    private float velocidadez;
    private float velocidade;
    public float velocidademax = 8;
    public float forcapulo = 300;

    public Slider[] Barras;
   // Inimigo inimigo;
    public GameObject cam;
    public Animator animcam;
    Animator anim;
    Rigidbody rb;
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


    #endregion

    void Start () 
    {
        
        rb = GetComponent<Rigidbody>();
       // inimigo = GetComponent<Inimigo>();
        anim = GetComponent<Animator>();

        Vida = 200;
        Toxina = 0;
        MP = 0; 

        Redutor = 2;

        Clareando = 1;
        RedutorBrilho = .05f;

        TempoDeDano = 1;
        RedutorDano = 0.05f;

        velocidadex = velocidademax;
        velocidadez = velocidademax;
	}
	
	void Update () 
    {
        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));

        anim.SetBool("NoChao", nochao);
        anim.SetFloat("VelocidadeY", rb.velocity.y);

        if (Input.GetButton("Jump") && nochao)
        {
            pulo = true;
        }

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

        if (Vida >= 100)
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
        
        if (!tamorto)
        {

            if (TomouDano)
            {
                anim.SetBool("Dano", true);
                if (Combo >= 4)
                    anim.SetBool("KO", true);
            }

            else
            {
                Combo = 0;
                anim.SetBool("Dano", false);
                anim.SetBool("KO", false);



                Barras[0].value = Vida;
                Barras[1].value = MP;
                Barras[2].value = Toxina;
                Barras[3].value = Toxina;

               Toxina += Redutor * Time.deltaTime;

#region Movimento

               float eixox = Input.GetAxisRaw("Horizontal");
               float eixoz = Input.GetAxisRaw("Vertical");
                
                rb.velocity = new Vector3(eixox * velocidadex, rb.velocity.y, eixoz * velocidadez);
                float limiteXmin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 5)).x+1f;
                float limiteXmax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 5)).x-1f;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, limiteXmin, limiteXmax), transform.position.y, transform.position.z);
                if (eixox > 0 && !olhad)
                    Flip();
                else if (eixox < 0 && olhad)
                    Flip();

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

                if (transform.position.z <= -9f && eixoz < 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);

                if (transform.position.z <= -9f && eixoz > 0)
                    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);


                //if (transform.position.x <= Camera.main.transform.position.x && eixox < 0)
                //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);

                //if (transform.position.x >= Camera.main.transform.position.x && eixox > 0)
                //    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                #endregion

                #region Animação

                if (rb.velocity.x == 0 && rb.velocity.z == 0)
                    anim.SetInteger("Movimento", 0);

                if (rb.velocity.x != 0 || rb.velocity.z != 0)
                    anim.SetInteger("Movimento", 1);



                if (Input.GetButton("Fire1") && !Atacando)
                {
                    anim.SetTrigger("Atacar");
                    Atacando = true;
                    Acertou = true;
                    audios[0].Play();
                }


                if (Input.GetButtonDown("Fire2") && MP > 25 && !Atacando)
                {
                    anim.SetTrigger("Skill1");
                    Atacando = true;
                }


                if (Input.GetButtonDown("Fire3") && MP > 80 && !Atacando)
                {
                    anim.SetTrigger("Skill2");
                    Atacando = true;
                    audios[3].Play();
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

    void OnTriggerEnter(Collider Encosta)
    {
        if (Encosta.gameObject.tag == "TagAtaqueIn")
        {
            if (TomouDano)
                Combo++;
            TomouDano = true;
            Vida -= 5;
            audios[1].Play();
        }
        if (Encosta.gameObject.tag == "TagAtaqueInBoss")
        {
            if (TomouDano)
                Combo++;
            TomouDano = true;
            Vida -= 15;
            audios[1].Play();
        }
        if (Encosta.gameObject.tag == "TagAtaqueInBoss2")
        {
            if (TomouDano)
                Combo = 4;
            anim.SetBool("KO", true);
            TomouDano = true;
            Vida -= 30;
            Impulso();
            audios[1].Play();

        }
            if (Encosta.gameObject.tag == "TagInimigo" && Acertou)
            MP += 10;
        if (Encosta.gameObject.tag == "Antidoto")
        {
            Toxina -= 25;
            Destroy(Encosta.gameObject);
        }

    }

    void Flip()
    {
        olhad = !olhad;
        transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
        rb.AddForce(new Vector3(-20, 0, 0), ForceMode.Impulse);

        else
        rb.AddForce(new Vector3(20, 0, 0), ForceMode.Impulse);
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
        audios[3].Play();
    }
}
