//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour {

    public GameObject ojogador;
    public Rigidbody rbcam;

    public Animator animcam;
    public float limitepositivo, limitenegativo;
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
        if (ojogador!=null)
        {
            float posx = ojogador.transform.position.x;
            if (ojogador.transform.position.x <= limitenegativo|| ojogador.transform.position.x >= limitepositivo)
            {

            }
            else
            {
                transform.position = new Vector3(posx, transform.position.y, transform.position.z);
                //limitenegativo += 0.5f;
            }
            
        }
       
         
            

        

    #region Metodo Alternativo
    //float eixox = Input.GetAxisRaw("Horizontal");
    //rbcam.velocity = new Vector3(eixox * velocidadecam, rbcam.velocity.y, rbcam.velocity.z);
    #endregion

}
}
