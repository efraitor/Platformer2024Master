using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    //M�todo para detectar cuando un GO ha entrado en la zona de StompBox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el GO es un enemigo
        if (collision.CompareTag("Enemy"))
        {
            //Mensaje para saber si hemos pisado al enemigo
            //Debug.Log("Hit Enemy");
            //Llamamos al m�todo que elimina al enemigo ya que podemos acceder a sus propiedades a trav�s de su Collider
            collision.gameObject.GetComponentInParent<EnemyDeath>().EnemyDeathController();
            //Llamamos al m�todo que hace rebotar al jugador que est� en el objeto padre
            GetComponentInParent<PlayerController>().Bounce(GetComponentInParent<PlayerController>().bounceForce);
        }
    }
}
