using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    [Tooltip("Current Speed is incremented by this value until 'Speed' is reached. Leave as 0 for no acceleration.")]
    private float acceleration;

    [SerializeField]
    [Tooltip("Current Speed is decremented by this value when movement keys are released. Leave as 0 for no deceleration")]
    private float deceleration;

    [SerializeField]
    [Tooltip("Set diagonal speed.")]
    private float diagonalSpeed;

    private float currentSpeed;

    private float inputX;

    private float inputY;


    private float lastInputX;

    private float lastInputY;

    private Rigidbody2D rb;

    private Animator animator;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentSpeed = 0;
	}
	
	void FixedUpdate ()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        float maxSpeed = speed;

        float velocityX = rb.velocity.x / maxSpeed;
        float velocityY = rb.velocity.y / maxSpeed;


        animator.SetFloat("Horizontal", velocityX);
        animator.SetFloat("Vertical", velocityY);

        // If diagonal movement: use diagonal max speed
        if (Mathf.Abs(inputX) == Mathf.Abs(inputY))
        {
            maxSpeed = diagonalSpeed;
        }

        // Check for input and apply acceleration/deceleration if set.
        if (inputX != 0 || inputY != 0)
        {
            if (acceleration != 0)
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
            else
            {
                currentSpeed = maxSpeed;
            }

            rb.velocity = new Vector3(inputX, inputY) * currentSpeed;

            lastInputX = inputX;
            lastInputY = inputY;
        }
        //Deceleration. (Not a good solution.)
        else if (inputX == 0 && inputY == 0)
        {
            if (deceleration != 0)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration;
                }
                else if (currentSpeed < 0)
                {
                    currentSpeed = 0;
                }
            }
            else
            {
                currentSpeed = 0;
            }

            //print("Current Speed: " + currentSpeed);
            rb.velocity = new Vector3(lastInputX, lastInputY) * currentSpeed;
        }
        else
        {
            rb.velocity = new Vector3(inputX, inputY) * currentSpeed;
        }
    }

    public void SetAnimation(bool right, bool left, bool up, bool down)
    {
        animator.SetBool("isRight", right);
        animator.SetBool("isLeft", left);
        animator.SetBool("isUp", up);
        animator.SetBool("isDown", down);
    }
}
