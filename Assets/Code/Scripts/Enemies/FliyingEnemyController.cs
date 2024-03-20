using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliyingEnemyController : MonoBehaviour
{
    //Array de puntos por los que se mueve el enemigo
    public Transform[] points;
    //Velocidad de movimiento del enemigo
    public float moveSpeed;
    //Variable para conocer el punto del recorrido en el que se encuentra el enemigo
    public int currentPoint;

    //Distancia del jugador para poder atacar, velocidad de persecución
    public float distanceToAttackPlayer, chaseSpeed;
    //Objetivo del enemigo
    private Vector3 attackTarget;

    //Tiempo entre ataques
    public float waitAfterAttack;
    //Contador de tiempo entre ataques
    private float _attackCounter;

    //Referencia al SpriteRenderer del enemigo
    private SpriteRenderer _sR;
    //Referencia al PlayerController
    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos la referencia al SpriteRenderer del enemigo
        _sR = GetComponentInChildren<SpriteRenderer>(); //Lo sacamos del hijo
        //Inicializamos la referencia al PlayerController
        _player = GameObject.Find("Player");

        //Hacemos que los puntos entre los que se mueve el enemigo dejen de tener padre para que no lo sigan
        foreach(Transform p in points)
        {
            p.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de tiempo entre ataques aún está lleno
        if (_attackCounter > 0)
            //Hacemos que se vacíe el contador
            _attackCounter -= Time.deltaTime;
        //Si el contador de tiempo entre ataques ya está vacío
        else
        {
            //Si la distancia entre el jugador y el enemigo es suficientemente grande
            if (Vector3.Distance(transform.position, _player.transform.position) > distanceToAttackPlayer)
            {
                //Reiniciamos el objetivo del ataque
                attackTarget = Vector3.zero;

                //Movemos al enemigo
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                //Si el enemigo prácticamente ha llegado a su punto de destino
                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.01f)
                {
                    //Pasamos al siguiente punto
                    currentPoint++;

                    //Comprobamos si hemos llegado al último punto del array
                    if (currentPoint >= points.Length)
                        //Reseteamos al primer punto del array
                        currentPoint = 0;
                }

                //Si el enemigo ha llegado al punto más a la izquierda
                if (transform.position.x < points[currentPoint].position.x)
                    //Rotamos al enemigo para que mire en dirección contraria
                    _sR.flipX = true;
                //Si el enemigo ha llegado al punto más a la derecha
                else if (transform.position.x > points[currentPoint].position.x)
                    //Dejamos al enemigo mirando a la izquierda
                    _sR.flipX = false;
            }
            //Si por el contrario el jugador está lo suficientemente cerca como para ser atacado
            else
            {
                //Si el objetivo del ataque está vacío
                if (attackTarget == Vector3.zero)
                    //El objetivo del ataque será el jugador
                    attackTarget = _player.transform.position;

                //Movemos al enemigo hacia donde está el jugador
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                //Si el enemigo está a la izquierda del punto al que tiene que ir
                if (transform.position.x < attackTarget.x)
                    //Rotamos al enemigo para que mire en dirección contraria
                    _sR.flipX = true;
                //Si el enemigo está a la derecha del punto al que tiene que ir
                else if (transform.position.x > attackTarget.x)
                    //Dejamos al enemigo mirando a la izquierda
                    _sR.flipX = false;

                //Si el enemigo ha llegado prácticamente a la posición objetivo del ataque
                if (Vector3.Distance(transform.position, attackTarget) <= 0.1f)
                {
                    //Inicializamos el contador de tiempo entre ataques
                    _attackCounter = waitAfterAttack;
                    //Reiniciamos el objtivo del ataque
                    attackTarget = Vector3.zero;
                }
            }
        }
    }
}
