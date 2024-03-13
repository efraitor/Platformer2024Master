using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    //Referencia al LevelManager
    private LevelManager _lMReference;

    private void Awake()
    {
        _lMReference = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si el que entra es el jugador
        if (collision.CompareTag("Player"))
            //Llamar al método que finaliza el nivel
            //Debug.Log("Finish Level");
            _lMReference.ExitLevel();

    }
}
