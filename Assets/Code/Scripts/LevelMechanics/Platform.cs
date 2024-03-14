using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //Referenciamos el collider de la plataforma
    Collider2D _platformCollider;
    //Variable que nos permite usar bajar de la plataforma si estamos sobre ella
    bool _onPlatform = false;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el collider de la plataforma
        _platformCollider = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si pulsamos abajo y estamos sobre la plataforma
        if(Input.GetAxisRaw("Vertical") < -0.5f && _onPlatform)
        {
            //Llamamos a la corrutina que activa y desactiva la plataforma
            StartCoroutine(ActDeactPlatformCo());
        }
    }

    //Mientras un collider esté tocando al de este objeto
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Si el que está colisionando es el jugador
        if (collision.gameObject.CompareTag("Player"))
            //Estamos sobre la plataforma
            _onPlatform = true;
    }

    //Método para saber cuando algo ha dejado de colisionar
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el que deja de colisionar es el jugador
        if (collision.gameObject.CompareTag("Player"))
            //No estamos sobre la plataforma
            _onPlatform = false;
    }

    //Corrutina que activa y desactiva el collider de la plataforma
    private IEnumerator ActDeactPlatformCo()
    {
        //Desactivamos el componente collider
        _platformCollider.enabled = false; //Enabled nos permite activar o desactivar un componente específico del objeto
        //Esperamos una cantidad de tiempo específica
        yield return new WaitForSeconds(.5f);
        //Activamos el componente collider
        _platformCollider.enabled = true;
    }
}
