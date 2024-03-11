using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSCamera : MonoBehaviour
{
    //Puntos mínimos y máximos entre los que se puede mover la cámara
    public Vector2 minPos, maxPos;

    //Referencia al target de la cámara
    public Transform target;

    void LateUpdate()//Ponemos LateUpdate para que este método se reproduzca después del Update del jugador evitando tirones de la cámara
    {
        //Creamos una variable con la restricción de la posición en X entre un mínimo y máximo
        float xPos = Mathf.Clamp(target.position.x, minPos.x, maxPos.x);
        //Creamos una variable con la restricción de la posición en Y entre un mínimo y máximo
        float yPos = Mathf.Clamp(target.position.y, minPos.y, maxPos.y);

        //Ahora teniendo en cuenta las restricciones movemos la cámara siguiendo al jugador
        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
