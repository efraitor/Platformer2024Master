using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerPad : MonoBehaviour
{
    //Referencia al Animator del Bouncer
    private Animator _anim;

    //Fuerza de empuje del Bouncer
    public float bounceForce = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al Animator
        _anim = GetComponent<Animator>();
    }

    //Método para conocer cuando un objeto se mete en la zona de rebote
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de rebote
        if (collision.CompareTag("Player"))
        {
            //Hacemos que el jugador rebote
            collision.GetComponent<PlayerController>().Bounce(bounceForce);
            //Activamos el Trigger de la animación
            _anim.SetTrigger("Bounce");
        }
    }
}
