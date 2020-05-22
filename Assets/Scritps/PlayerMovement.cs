using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Vector3 input;
    public Vector3 velocity;
    Vector3 movement;

    public float movementSpeed = 5;
    public float gravity = -20;
    public float jumpHeight = 1.5f;

    public Transform groundCheck;
    float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    PlayerAttributes playerAttributes;

    private void Start()
    {
        playerAttributes = GameObject.Find("First-Person Player").GetComponent<PlayerAttributes>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2;
            }

            GetInput();
            SpecialActions();
        }
        else
        {
            if (input.y > 0)
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

        input = new Vector3(x, 0, z);
    }

    void SpecialActions()
    {
        if (playerAttributes.hunger == playerAttributes.minHunger || playerAttributes.energy == playerAttributes.minEnergy)
        {
            movementSpeed = 1;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && playerAttributes.stamina > playerAttributes.minStamina)
            {
                movementSpeed = 10;
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                movementSpeed = 2.5f;
            }

            if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftShift))
            {
                movementSpeed = 5;
            }

            if (Input.GetButtonDown("Jump") && movementSpeed == 5 && playerAttributes.stamina > playerAttributes.minStamina)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }
    }

    void Move()
    {
        if (isGrounded)
        {
            movement = transform.right * input.x + transform.forward * input.z;
        }

        movementSpeed = Mathf.Clamp(movementSpeed, 2.5f, 10);

        characterController.Move(movement * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }
}
