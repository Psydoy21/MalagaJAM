using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorReal : MonoBehaviour
{
   
    [HideInInspector]  Rigidbody2D rb;
    [SerializeField]  LayerMask suelo;
    [SerializeField] GameObject objeto;
    [SerializeField] SpriteRenderer spriteR;

    [HideInInspector] private float speed;
    [SerializeField] private float aceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxVelocity;
    [HideInInspector] public float move;


    private float tiempoEnElAire;
    [SerializeField] private float AlturaMax;
    private float numeroSaltos;
    [SerializeField] private float velocidadSalto;
    private Animator animator;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    void Update()
    {
        flipx();
        movimientoHorizontal();
        if(Mathf.Abs(move)>0)
        {
            animator.SetBool("andar", true);
        }
        else
        {
            animator.SetBool("andar", false);
        }
    }

    private void movimientoHorizontal()
    {
       
        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (move == 0)
        {
            if (Mathf.Abs(speed) <= 0.1f)
            {
                speed = 0;
            }
            else
            {
                if (speed > 0)
                {
                    speed = speed - deceleration * Time.deltaTime;
                }
                else
                {
                    speed = speed + deceleration * Time.deltaTime;
                }
            }
        }
        else
        {
            if (move * speed < 0)
            {
                speed = speed + deceleration * Time.deltaTime * move;
            }
            else
            {
                if (Mathf.Abs(speed) >= maxVelocity)
                {
                    speed = maxVelocity * move;
                }
                else
                {
                    speed = speed + aceleration * Time.deltaTime * move;
                }
            }
        }
    }
    private void movimientoVertical()
    {
        RaycastHit2D raycastSueloL = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.left * 0.65f), Vector2.down, 1f, suelo);
        RaycastHit2D raycastSueloR = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.right * 0.5f), Vector2.down, 1f, suelo);
        Debug.DrawRay((transform.position + Vector3.left * 0.65f + Vector3.down * 0.5f), Vector3.down * 1f, Color.green);
        Debug.DrawRay((transform.position + Vector3.right * 0.5f + Vector3.down * 0.5f), Vector3.down * 1f, Color.green);


        if (Input.GetKey(KeyCode.Space) && tiempoEnElAire < AlturaMax && numeroSaltos < 1)
        {
            tiempoEnElAire = tiempoEnElAire + Time.deltaTime;
            rb.velocity = new Vector2(rb.velocity.x, velocidadSalto);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            numeroSaltos = numeroSaltos + 1f;
        }



        if ((raycastSueloL == false) && (raycastSueloR == false) && Input.GetKey(KeyCode.Space) == false)
        {
            numeroSaltos = numeroSaltos + 6f * Time.deltaTime;
        }

        if (raycastSueloL == true || raycastSueloR == true)
        {
            tiempoEnElAire = 0f;
            numeroSaltos = 0f;
        }


    }
    public void interactuar()
    {
        if ( Input.GetKeyDown(KeyCode.E) == true)
        {
            objeto.SetActive(true);
        }
    }
    private void flipx()
    {
        if (rb.velocity.x < -0.1)
        {
            spriteR.flipX = true;
        }
        if (rb.velocity.x > 0.1)
        {
            spriteR.flipX = false;
        }
    }

}
