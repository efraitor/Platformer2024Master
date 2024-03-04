using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //Variable para saber si este objeto es una gema o una cura
    public bool isGem, isHeal;

    //Variable para conocer si un objeto ya ha sido recogido
    private bool _isCollected;

    //Referencia al efecto de recogida del objeto
    public GameObject pickupEffect;

    //Referencia al LevelManager
    private LevelManager _lMReference;
    //Referencian al UIController
    private UIController _uIReference;

    private void Start()
    {
        //Inicializamos la referencia al LevelManager
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //Inicializamos la referencia al UIController
        _uIReference = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    //Método para interactuar con los objetos al entrar en su zona
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es el jugador el que entra en la zona y el objeto no había sido recogido
        if (collision.CompareTag("Player") && !_isCollected)
        {
            //Si el objeto en este caso es una gema
            if (isGem)
            {
                //Sumo uno al contador de gemas
                _lMReference.gemCollected++;
                //Actualizamos el contador en la UI
                _uIReference.UpdateGemCount();
                //Le decimos que el objeto ha sido recogido
                _isCollected = true;
                //Instanciamos el efecto de recogida
                Instantiate(pickupEffect, transform.position, transform.rotation);
                //Destruimos el objeto
                Destroy(gameObject);
            }
        }
    }
}
