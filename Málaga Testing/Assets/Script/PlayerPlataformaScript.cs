using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlataformaScript : MonoBehaviour
{
   
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] public LayerMask suelo;

    [HideInInspector] private float speed;
    [SerializeField] private float aceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxVelocity;
    [HideInInspector] public float move;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    void Update()
    {
        movimientoHorizontal();
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
}
