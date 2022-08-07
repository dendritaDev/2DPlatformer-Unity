using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed;

    [Header("Salto")]
    private bool canDoubleJump;
    public float jumpForce;


    [Header("Componentes")]
    public Rigidbody2D theRB;

    [Header("Grounded")]
    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    [Header("Animator")]
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        //Checks if a Collider falls within a circular area.
        //The circle is defined by its centre coordinate in world space and by its radius. The optional layerMask allows the test to check only for objects on specific layers.

        if(isGrounded)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded)
            {
                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }
            else
            {
                if(canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
            
        }

        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); //aqui lo que hacemos es pasarle al animator el valor de movespeed para que valore si tiene que hacer una animacion o otra en funcion de las condicioensq ue pusimos
        anim.SetBool("isGrounded", isGrounded);
    }
}
