using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    //Lista de estados por los que puede pasar el jefe final (Máquina de estados)
    public enum bossStates { shooting, hurt, moving, ended};
    //Atributo de las variables que me permite asociar una descripción visible a la variable en el editor de Unity
    [Tooltip("Elegimos el estado actual en el que se encuentra el jefe final")]
    //Creamos una referencia al estado actual del jefe final
    public bossStates currentState;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Movement")]
    //Atributo de las variables que me permite asignar una barra de selección a una variable
    [Range(0, 100)]
    //Velocidad del jefe final
    public float moveSpeed;
    //Puntos entre los que se mueve el enemigo
    public Transform leftPoint, rightPoint;
    //Para conocer hacia donde se mueve
    private bool _movingRight;

    //Atributo de las variables que genera un encabezado en el editor de Unity
    [Header("Shooting")]
    //Referencia a los proyectiles del enemigo
    public GameObject bullet;
    //Punto desde el que salen los proyectiles
    public Transform firePoint;
    //Tiempo entre disparos
    public float timeBetweenShots;
    //Contador de tiempo entre disparos
    private float _shotCounter;

    [Header("Hurt")]
    //Tiempo de daño del enemigo
    public float hurtTime;
    //Contador de tiempo de daño
    private float _hurtCounter;
    //Referencia a la zona de daño del jefe final
    public GameObject hitBox;

    [Header("Health")]
    //Vida del enemigo
    public int health = 3;
    //Referencia al efecto de explosión del enemigo y a los objetos que aparecerán tras su muerte
    public GameObject explosion;
    //Variable para conocer si el enemigo ha sido derrotado
    private bool _isDefeated;


    [Header("References")]
    //Posición del Boos
    public Transform theBoss;
    //Referencia al Animator del jefe final
    private Animator _bAnim;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Animator del jefe final
        _bAnim = GetComponentInChildren<Animator>();
        //Inicializamos el estado en el que empieza el jefe final
        currentState = bossStates.shooting;//currentState = bossStates.shooting <=> currentState = 0
    }

    // Update is called once per frame
    void Update()
    {
        //MÁQUINA DE ESTADOS
        //En base a los cambios en el valor de enum generamos los cambios de estado
        switch (currentState)
        {
            //En el caso en el que currentState = 0
            case bossStates.shooting:
                //Hacemos decrecer el contador entre disparos
                _shotCounter -= Time.deltaTime;

                //Si el contador de tiempo entre disparos se ha vaciado
                if(_shotCounter <= 0)
                {
                    //Reiniciamos el contador de tiempo entre disparos
                    _shotCounter = timeBetweenShots;
                    //Instanciamos la bala pero en una nueva referencia cada vez
                    GameObject b = Instantiate(bullet, firePoint.position, firePoint.rotation);
                    //Como cada bala estará referenciada (será única) puedo aplicarle los cambios que queramos
                    //En este caso le diré a cada bala hacia donde debe apuntar según hacia donde mira el jefe final
                    b.transform.localScale = theBoss.localScale;
                }
                break;
            //En el caso en el que currentState = 1
            case bossStates.hurt:
                //Si el contador de tiempo de daño aún no está vacío
                if(_hurtCounter > 0)
                {
                    //Hacemos decrecer el contador de tiempo de daño
                    _hurtCounter -= Time.deltaTime;

                    //Si el contador de tiempo de daño se ha vaciado
                    if(_hurtCounter <= 0)
                    {
                        //El jefe final pasaría al estado de movimiento
                        currentState = bossStates.moving;

                        //Si el enemigo ha sido derrotado
                        if (_isDefeated)
                        {
                            //Desactivamos el tanque
                            theBoss.gameObject.SetActive(false);
                            //Instanciamos el efecto de explosión
                            Instantiate(explosion, theBoss.position, theBoss.rotation);
                            //El enemigo pasa al estado de muerte
                            currentState = bossStates.ended;
                        }
                    }
                }
                break;
            //En el caso en el que currentState = 2
            case bossStates.moving:
                //Si el enemigo se mueve a la derecha
                if (_movingRight)
                {
                    //Movemos al enemigo a una velocidad a la derecha
                    theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    //Si el enemigo ha llegado al punto de la derecha
                    if(theBoss.position.x > rightPoint.position.x)
                    {
                        //Usando la escala puedo hacer que gire todo el objeto 
                        theBoss.localScale = Vector3.one;  //new Vector3(1f, 1f, 1f) <=> Vector3.one
                        //Dejará de moverse a la derecha
                        _movingRight = false;
                        //Llamamos al método que frena al enemigo
                        EndMovement();
                    }
                }
                //Si el enemigo se mueve a la izquierda
                else
                {
                    //Movemos al enemigo a una velocidad a la izquierda
                    theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
                    //Si el enemigo ha llegado al punto de la izquierda
                    if (theBoss.position.x < leftPoint.position.x)
                    {
                        //Usando la escala puedo hacer que gire todo el objeto 
                        theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        //Dejará de moverse a la izquierda
                        _movingRight = true;
                        //Llamamos al método que frena al enemigo
                        EndMovement();
                    }
                }
                break;
            //En el caso en el que currentState = 3
            case bossStates.ended:
                Debug.Log("Ended");
                break;
        }

        //Para que este input solo funcione en el editor de Unity, no en la Build
#if UNITY_EDITOR
        //Si pulsamos la tecla H
        if (Input.GetKeyDown(KeyCode.H))
            //Llamamos al método que hace daño al jefe final
            TakeHit();
#endif
    }

    //Método para cuando el jefe final recibe daño
    public void TakeHit()
    {
        //El boss final cambiará al estado de recibir daño
        currentState = bossStates.hurt;
        //Inicializamos el contador de tiempo de daño
        _hurtCounter = hurtTime;
        //Activamos el trigger de la animación de daño
        _bAnim.SetTrigger("Hit");

        //Hacemos que el enemigo pierda una vida
        health--;
        //Si no le quedan vidas al enemigo
        if (health <= 0)
            //El enemigo ha sido derrotado
            _isDefeated = true;
    }

    //Método para finalizar el movimiento del jefe final
    public void EndMovement()
    {
        //El enemigo pasará al estado de ataque
        currentState = bossStates.shooting;
        //Ponemos a 0 o reiniciamos el contador de tiempo entre disparos
        _shotCounter = 0f;
        //Inicializamos el contador de tiempo entre disparos
        _shotCounter = timeBetweenShots;
        //Activamos el trigger de la animación de parada de movimiento
        _bAnim.SetTrigger("StopMoving");
        //Al terminar el movimiento volvemos a activar la HitBox del enemigo
        hitBox.SetActive(true);
    }

}
