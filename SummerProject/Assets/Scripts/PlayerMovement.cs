using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed;

    [SerializeField][Tooltip("Current Speed is incremented by this value until Max Speed is reached. Leave as 0 for no acceleration.")]
    private float acceleration;

    [SerializeField][Tooltip("Slows down or speeds up diagonal movement.")]
    private float diagonalSpeedMod;

    private float currentSpeed;

    private Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = 0;
	}
	
	void FixedUpdate ()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (acceleration != 0)
        {
            if (x != 0 || y != 0)
            {
                if (currentSpeed < maxSpeed)
                {
                    currentSpeed += acceleration;
                }
                else if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }
            }
            else if (x == 0 && y == 0)
            {
                currentSpeed = 0;
            }
        }
        else
        {
            currentSpeed = maxSpeed;
        }



        // If diagonal movement: add speed modifier
        if ((Mathf.Abs(x) != Mathf.Abs(y)))
        {
            rb.velocity = new Vector3(x, y) * currentSpeed;
        }
        else if (x != 0 || y != 0)
        {
            rb.velocity = new Vector3(x, y) * (currentSpeed + diagonalSpeedMod);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        

        

        
	}
}
