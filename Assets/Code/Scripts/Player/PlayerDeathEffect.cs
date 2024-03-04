using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEffect : MonoBehaviour
{
    //Referencia al Sprite Renderer del efecto de muerte
    private SpriteRenderer _sR;

    //Variable para saber hacia donde miraba el jugador
    public bool wasSeeLeft;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al SpriteRenderer
        _sR = GetComponent<SpriteRenderer>();

        //Girar el sprite de la muerte del jugador según a que lado esté mirando este
        //Si el jugador mira a la izquierda
        if (wasSeeLeft == true)
            //No cambiamos la dirección del sprite
            _sR.flipX = false;
        //Si por el contrario el jugador está mirando a la derecha
        else
            //Le damos la vuelta al efecto de muerte
            _sR.flipX = true;
    }
}
