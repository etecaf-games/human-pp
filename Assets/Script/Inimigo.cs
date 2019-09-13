using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour {

    private Rigidbody inimrb;
    private Animator inimanim;
    public Transform checachao;
    public Transform alvo;

    public float distanciaalvox;
    public float distanciaalvoz;
    private Vector3 distanciaalvo;

    private float tempo = Time.deltaTime;
    private bool nochao;
    private bool olhad = true;
    private bool inimtamorto = false;
    private bool parado;

	// Use this for initialization
	void Start () {

        inimrb = GetComponent<Rigidbody>();
        inimanim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update()
    {
        inimanim.SetFloat("InVelocidade", (distanciaalvo.magnitude));

        nochao = Physics.Linecast(transform.position, checachao.position, 3 << LayerMask.NameToLayer("CamadaChão"));
        inimanim.SetBool("InNoChao", nochao);

        if (olhad)
            transform.localScale = new Vector3(3, 3, 1);
        else
            transform.localScale = new Vector3(-3, 3, 1);

        //olhad = (alvo.position.x < transform.position.x) ? false : true;

        if (alvo.position.x < transform.position.x)
            olhad = false;
        else
            olhad = true;

        distanciaalvox = alvo.position.x - transform.position.x;
        distanciaalvoz = alvo.position.z - transform.position.z;
        distanciaalvo = alvo.position - transform.position;
    }


    void FixedUpdate()
    {
        if (!inimtamorto)
        {
            if (alvo.position.x < transform.position.x)
                transform.position = new Vector3(transform.position.x - 2.5f * tempo, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + 2.5f * tempo, transform.position.y, transform.position.z);

            if (alvo.position.z < transform.position.z)
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 2.5f * tempo);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2.5f * tempo);
        }
        if (distanciaalvox <= 2 && distanciaalvox >= -2 && distanciaalvoz <= 2 && distanciaalvoz >= -2)
            tempo = 0;
        else
            tempo = Time.deltaTime;
    }
}
