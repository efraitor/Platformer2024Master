using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Método para detectar cuando un GO ha entrado en la zona de StompBox
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el GO es un enemigo
        if (collision.CompareTag("Enemy"))
        {
            //Mensaje para saber si hemos pisado al enemigo
            //Debug.Log("Hit Enemy");
            //Llamamos al método que elimina al enemigo ya que podemos acceder a sus propiedades a través de su Collider
            collision.gameObject.GetComponentInParent<EnemyDeath>().EnemyDeathController();
            //Llamamos al método que hace rebotar al jugador que está en el objeto padre
            GetComponentInParent<PlayerController>().Bounce();
        }
    }
}
