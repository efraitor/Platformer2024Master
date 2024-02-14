using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Velocidad del jugador
    public float moveSpeed;
    //El rigidbody del jugador
    //Barrabaja indica que la variable es privada
    private Rigidbody2D _theRB;
    //Fuerza de salto del jugador
    public float jumpForce;

    //Variable para saber si el jugador está en el suelo
    private bool _isGrounded;
    //Referencia al punto por debajo del jugador que tomamos para detectar el suelo
    public Transform groundCheckPoint;
    //Referencia para detectar el Layer de suelo
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el Rigidbody del jugador
        //GetComponent => Va al objeto donde está metido este código y busca el componente indicado
        _theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //El jugador se mueve a una velocidad dada en X, y la velocidad que ya tuviera en Y
        _theRB.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, _theRB.velocity.y);

        //La variable isGrounded se hará true siempre que el círculo físico que hemos creado detecte suelo, sino será falsa
        //OverlapCircle(punto donde se genera el círculo, radio del círculo, layer a detectar)
        _isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        //Si pulsamos el botón de salto
        if (Input.GetButtonDown("Jump"))
        {
            //Si el jugador está en el suelo
            if (_isGrounded)
            {
                //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza de salto
                _theRB.velocity = new Vector2(_theRB.velocity.x, jumpForce);
            }
        }
    }
}
