using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    //Atributo de las variables que me permite asignar una barra de selecci�n a una variable
    [Range(0, 10)]
    //Velocidad de los proyectiles
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        //Movemos a la bala en horizontal, usamos el LocalScale para saber a donde debe apuntar la bala (derecha o izquierda)
        transform.position += new Vector3(-moveSpeed * transform.localScale.x * Time.deltaTime, 0f, 0f);
    }

    //Cuando un objeto entra en la zona de la bala
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona de la bala
        if (collision.CompareTag("Player"))
            //El jugador recibir� da�o
            collision.GetComponent<PlayerHealthController>().DealWithDamage();
        //Destruimos la bala
        Destroy(gameObject);
    }
}
