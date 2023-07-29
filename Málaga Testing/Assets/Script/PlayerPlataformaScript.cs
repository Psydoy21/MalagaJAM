using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlataformaScript : MonoBehaviour
{
    [Header("Componentes")]
    [HideInInspector]  Rigidbody2D rb;
    [SerializeField]  LayerMask suelo;
    [SerializeField] objetoInteractuable objetoInteractuable;
    [SerializeField] GameObject coin;
    [SerializeField] Animator Animator;
    [SerializeField] SpriteRenderer spriteR;

    [Header("Movimiento Horizontal")]

    [HideInInspector] private float speed;
    [SerializeField] private float aceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxVelocity;
    [SerializeField] public float move;

    [Header("Salto")]

    private float tiempoEnElAire;
    [SerializeField] private float AlturaMax;
    private float numeroSaltos;
    [SerializeField] private float velocidadSalto;

    //---------------Valores booleans--------------
    [HideInInspector] public bool objetoCogido = false;
    [HideInInspector] Vector3 posicionInicial;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        posicionInicial = transform.position;
    }
    void Update()
    {
        
        
        movimientoHorizontal();
        movimientoVertical();
        interactuar();
        animationControler();
        flipx();
            
        
        
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
        RaycastHit2D raycastSueloL = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.left * 0.3f), Vector2.down, 0.7f, suelo);
        RaycastHit2D raycastSueloR = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.right * 0.3f), Vector2.down, 0.7f, suelo);
        Debug.DrawRay((transform.position + Vector3.left * 0.3f + Vector3.down * 0.5f), Vector3.down * 0.7f, Color.green);
        Debug.DrawRay((transform.position + Vector3.right * 0.3f + Vector3.down * 0.5f), Vector3.down * 0.7f, Color.green);


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
        if (objetoInteractuable.interactuable == true && Input.GetKeyDown(KeyCode.E) == true)
        {
            coin.SetActive(true);
            objetoInteractuable.desactivarObjeto();
            objetoCogido = true;
        }
    }
    private void animationControler()
    {
        RaycastHit2D raycastSueloL = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.left * 0.65f), Vector2.down, 1f, suelo);
        RaycastHit2D raycastSueloR = Physics2D.Raycast((transform.position + Vector3.down * 0.5f + Vector3.right * 0.5f), Vector2.down, 1f, suelo);

        if (move == 0)
        {
            Animator.SetBool("moving", false);

        }
        else
        {
            Animator.SetBool("moving", true);
        }
        if (rb.velocity.y < 0)
        {
            Animator.SetBool("falling", true);
            Animator.SetBool("jumping", false);
        }
        if (rb.velocity.y > 0)
        {
            Animator.SetBool("falling", false);
            Animator.SetBool("jumping", true);
        }
        if (raycastSueloL || raycastSueloR)
        {
            Animator.SetBool("falling", false);
            Animator.SetBool("jumping", false);
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
    public void bloquearElPersonaje()
    {
        rb.simulated = false;
    }
    public void deathPlane()
    {
        gameObject.transform.position = posicionInicial;
    }
    public void puerta()
    {
        Animator.SetTrigger("puerta");
    }

}
