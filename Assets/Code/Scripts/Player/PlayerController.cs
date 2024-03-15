using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region VARIABLES AND REFERENCES
    //Velocidad del jugador
    public float moveSpeed;
    //Fuerza de salto del jugador
    public float jumpForce;
    //Fuerza de rebote del jugador
    public float bounceForce;

    //Variable para saber si el jugador est� en el suelo
    private bool _isGrounded;
    //Referencia al punto por debajo del jugador que tomamos para detectar el suelo
    public Transform groundCheckPoint;
    //Referencia para detectar el Layer de suelo
    public LayerMask whatIsGround;
    //Variable para saber si podemos hacer un doble salto
    private bool _canDoubleJump;

    //Variable para la fuerza del KnockBack
    public float knockBackForce;
    //Variables para controlar el contador de tiempo de Knocback
    public float knockBackLength; //Variable que nos sirve para rellenar el contador
    private float _knockBackCounter; //Contador de tiempo

    //Variable para conocer hacia donde mira el jugador
    public bool seeLeft = true;

    //Variable para saber cuando el jugador puede interactuar con los objetos
    public bool canInteract = false;


    //El rigidbody del jugador
    //Barrabaja indica que la variable es privada
    private Rigidbody2D _theRB;
    //Referencia al Animator del jugador
    private Animator _anim;
    //Referencia al SpriteRenderer del jugador
    private SpriteRenderer _theSR;
    #endregion

    #region UNITY METHODS
    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody del jugador
        //GetComponent => Va al objeto donde est� metido este c�digo y busca el componente indicado
        _theRB = GetComponent<Rigidbody2D>();
        //Inicializamos el Animator del jugador
        _anim = GetComponent<Animator>();
        //Inicializamos el SpriteRenderer del jugador
        _theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de KnockBack se ha vaciado, el jugador recupera el control del movimiento
        if(_knockBackCounter <= 0)
        {
            //El jugador se mueve a una velocidad dada en X, y la velocidad que ya tuviera en Y
            _theRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _theRB.velocity.y);

            //La variable isGrounded se har� true siempre que el c�rculo f�sico que hemos creado detecte suelo, sino ser� falsa
            //OverlapCircle(punto donde se genera el c�rculo, radio del c�rculo, layer a detectar)
            _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            //Si pulsamos el bot�n de salto
            if (Input.GetButtonDown("Jump"))
            {
                //Si el jugador est� en el suelo
                if (_isGrounded)
                {
                    //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                    _theRB.velocity = new Vector2(_theRB.velocity.x, jumpForce);
                    //Llamamos al m�todo del Singleton de AudioManager que reproduce el sonido
                    AudioManager.audioMReference.PlaySFX(10);
                    //Una vez en el suelo, reactivamos la posibilidad de doble salto
                    _canDoubleJump = true;
                }
                //Si el jugador no est� en el suelo
                else
                {
                    //Si canDoubleJump es verdadera
                    if (_canDoubleJump)
                    {
                        //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                        _theRB.velocity = new Vector2(_theRB.velocity.x, jumpForce);
                        //Llamamos al m�todo del Singleton de AudioManager que reproduce el sonido
                        AudioManager.audioMReference.PlaySFX(10);
                        //Hacemos que no se pueda volver a saltar de nuevo
                        _canDoubleJump = false;
                    }
                }
            }
            //Girar el Sprite del Jugador seg�n su direcci�n de movimiento(velocidad)
            //Si el jugador se mueve hacia la izquierda
            if (_theRB.velocity.x < 0)
            {
                //No cambiamos la direcci�n del sprite
                _theSR.flipX = false;
                //El jugador mira a la izquierda
                seeLeft = true;
            }
            //Si el jugador se mueve hacia la derecha
            else if (_theRB.velocity.x > 0)
            {
                //Cambiamos la direcci�n del sprite
                _theSR.flipX = true;
                //El jugador mira a la derecha
                seeLeft = false;
            }
        }
        //Si por el contrario el contador de KnockBack todav�a no est� vac�o
        else
        {
            //Hacemos decrecer el contador en 1 cada segundo
            _knockBackCounter -= Time.deltaTime;
            //Si el jugador mira a la izquierda
            if (!_theSR.flipX)
                //Aplicamos un peque�o empuje hacia la derecha
                _theRB.velocity = new Vector2(knockBackForce, _theRB.velocity.y);
            //Si el jugador mira a la derecha
            else
                //Aplicamos un peque�o empuje hacia la izquierda
                _theRB.velocity = new Vector2(-knockBackForce, _theRB.velocity.y);
        }
        
        //ANIMACIONES DEL JUGADOR
        //Cambiamos el valor del par�metro del Animator "moveSpeed", dependiendo del valor en X de la velocidad del Rigidbody
        _anim.SetFloat("moveSpeed", Mathf.Abs(_theRB.velocity.x));//Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda tambi�n se anime esta acci�n
        //Cambiamos el valor del par�metro del Animator "isGrounded", dependiendo del valor de la booleana del c�digo "_isGrounded"
        _anim.SetBool("isGrounded", _isGrounded);
    }

    //M�todo para conocer cuando un objeto entra en colisi�n con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el que colisiona contra el jugador es una plataforma
        if (collision.gameObject.CompareTag("Platform"))
            //El jugador pasa a ser hijo de la plataforma
            transform.parent = collision.transform;
    }

    //M�todo para conocer cuando dejamos de colisionar contra un objeto
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Si el objeto con el que dejamos de colisionar es una plataforma
        if (collision.gameObject.CompareTag("Platform"))
            //El jugador deja de tener padre
            transform.parent = null;
    }

    #endregion

    #region OWN METHODS
    //M�todo para gestionar el KnockBack producido al jugador al hacerse da�o
    public void Knockback()
    {
        //Inicializamos el contador de KnockBack
        _knockBackCounter = knockBackLength;
        //Paralizamos al jugador en X y hacemos que salte en Y
        _theRB.velocity = new Vector2(0f, knockBackForce);
        //Cambiamos el valor del par�metro del Animator "hurt"
        _anim.SetTrigger("hurt");
    }

    //M�todo para que el jugador rebote
    public void Bounce(float bounceForce)
    {
        //Impulsamos al jugador rebotando
        _theRB.velocity = new Vector2(_theRB.velocity.x, bounceForce);
    }
    #endregion
}
