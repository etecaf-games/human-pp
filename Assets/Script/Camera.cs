using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public Transform ojogador;
    public Rigidbody rbcam;

    public Animator animcam;

    //public float velocidadecam = 8;


    public bool ativo = false;

    void Awake()
    {
        ativo = true;
    }

	// Use this for initialization
	void Start () {

        //animcam = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        float posx = ojogador.position.x;
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);

        #region Metodo Alternativo
        //float eixox = Input.GetAxisRaw("Horizontal");
        //rbcam.velocity = new Vector3(eixox * velocidadecam, rbcam.velocity.y, rbcam.velocity.z);
        #endregion

    }
}
