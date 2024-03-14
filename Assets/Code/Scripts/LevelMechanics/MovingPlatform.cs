using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //Array de puntos por los que se mueve la plataforma móvil
    public Transform[] points;
    //Velocidad de movimiento de la plataforma
    public float moveSpeed;
    //Variable para conocer en que punto del recorrido se encuentra la plataforma
    public int currentPoint;

    //Referencia a la posición de la plataforma
    public Transform _platformPosition;

    // Update is called once per frame
    void Update()
    {
        //Movemos la plataforma hasta el punto actual al que queremos ir, a una velocidad dada
        _platformPosition.position = Vector3.MoveTowards(_platformPosition.position, points[currentPoint].position, moveSpeed * Time.deltaTime);
    
        //Si la plataforma prácticamente ha llegado a su punto de destino
        if(Vector3.Distance(_platformPosition.position, points[currentPoint].position) < 0.01f)
        {
            //Pasamos al siguiente punto
            currentPoint++;

            //Comprobamos si hemos llegado al último punto del array
            if (currentPoint >= points.Length)
                //Reseteamos al primer punto del array
                currentPoint = 0;
        }
    }
}
