using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    Vector3 input;
    Vector3 velocity;
    Vector3 movement;

    float movementSpeed = 5f;
    float gravity = -20f;
    float jumpHeight = 1.5f;

    public Transform groundCheck;
    float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            GetInput();
            SpecialActions();
        }
        else
        {
            if (input.y > 0f)
            {
                if ((characterController.collisionFlags & CollisionFlags.Above) != 0)
                {
                    velocity.y = -velocity.y;
                }
            }
        }

        Move();
    }

    void GetInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        input = new Vector3(x, 0f, z);
    }

    void SpecialActions()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = 10f;
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            movementSpeed = 2.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 5f;
        }

        if(movementSpeed == 5f)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    void Move()
    {
        if (isGrounded)
        {
            movement = transform.right * input.x + transform.forward * input.z;
        }
        
        movementSpeed = Mathf.Clamp(movementSpeed, 2.5f, 10f);

        characterController.Move(movement * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
