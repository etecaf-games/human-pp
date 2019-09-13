using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{
    #region Variaveis

    private float velocidadex;
    private float velocidadez;
    public float velocidademax = 8;
    public float forcapulo = 300;


    public GameObject cam;
    public Animator animcam;
    private bool treme =  false;

    private float velocidade;
    private Rigidbody rb;
    private Animator anim;
    public Transform checachao;
    public Transform Esquerda;
    public Transform Direita;
    private bool nochao;
    private bool tamorto = false;
    private bool olhad = true;
    private bool pulo = false;

    public bool checaesquerda = false;
    public bool checadireita = false;
    public bool checalados = false;

    #endregion

    void Awake()
    {
        Debug.Log("awake");
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () 
    {
        //animcam = cam.GetComponent<Animator>();
        //anim = GetComponent<Animator>();
        //checachao = gameObject.transform.Find("ChecaChao");

        rb = GetComponent<Rigidbody>();
        velocidadex = velocidademax;
        velocidadez = velocidademax;
	}
	
	// Update is called once per frame
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
        if (!tamorto)

        #region Movimento
        {
            float eixox = Input.GetAxisRaw("Horizontal");
            float eixoz = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector3(eixox * velocidadex, rb.velocity.y, eixoz * velocidadez);

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

        #region Animação


            checaesquerda = Physics.Linecast(transform.position, Esquerda.position, 3 << LayerMask.NameToLayer("CamadaParede"));
            checadireita = Physics.Linecast(transform.position, Direita.position, 3 << LayerMask.NameToLayer("CamadaParede"));


            if (checaesquerda || checadireita)
                checalados = true;
            else
                checalados = false;

            if (checaesquerda || checadireita)
            {
                velocidadez = 0;
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    velocidadez = velocidademax;
                }

                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    velocidadez = velocidademax;
                }
            }
            else
            {
                velocidadez = velocidademax;
            }

            if (eixox == 0 && eixoz == 0)
                anim.SetInteger("Movimento",0);

            else if (eixox != 0 || eixoz != 0 && !checalados)
                anim.SetInteger("Movimento", 1);

            else if (eixox == 0 && eixoz != 0 && checalados)
                anim.SetInteger("Movimento", 0);


            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Atacar");
            }

            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("Skill1");
            }

            if (Input.GetButtonDown("Fire3"))
            {
                anim.SetTrigger("Skill2");
                treme = true;
                if (treme)
                    animcam.SetBool("Vibra", true);
            }
            else
            {
                treme = false;
                animcam.SetBool("Vibra", false);
            }


            #endregion

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
 
}
