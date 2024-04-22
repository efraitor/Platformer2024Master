using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitBox : MonoBehaviour
{
    //Referencia al BossTankController
    private BossTankController _bossCont;

    private void Start()
    {
        //Inicializamos la referencia al BossTankController
        _bossCont = GameObject.Find("BossBattle").GetComponent<BossTankController>();
    }

    //Si algo entra en la HitBox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el que entra en la HitBox es el jugador
        if(collision.CompareTag("Player") && collision.transform.position.y > transform.position.y)
        {
            //Llamamos al método que hace daño al jefe final
            _bossCont.TakeHit();
            //Hacemos rebotar al jugador
            collision.GetComponent<PlayerController>().Bounce(8.0f);
            //Desactivamos la HitBox
            gameObject.SetActive(false);
        }
    }
}
