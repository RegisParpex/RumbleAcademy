using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private CharacterController characterController;
    public bool isGrounded;
    public bool isGroundedInWall;
    public float gravity;
    private float fallSpeed;
    public float jumpSpeed;
    public float moveSpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        /*
        IsGrounded();
        IsGroundedInWall();
        Fall();
        Jump();
        WallJump();
        Move();
        Slide();
        */
    }

    void Move()
    {
        float xSpeed = Input.GetAxis("Player1_JoystickLeftX");
        if(xSpeed != 0)
        {
            characterController.Move(new Vector3(xSpeed, 0, 0) * moveSpeed * Time.deltaTime);
        }
    }

    void Slide()
    {
        if (InputManager.Player1_X() && isGrounded)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void WallJump()
    {
        if (InputManager.Player1_A() && isGroundedInWall)
        {
            fallSpeed = -jumpSpeed;
        }
    }

    void Jump()
    {
        if(InputManager.Player1_A() && isGrounded)
        {
            fallSpeed = -jumpSpeed;
        }
    }
    void Fall()
    {
        if(!isGrounded)
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        else
        {
            if(fallSpeed > 0)
            {
                fallSpeed = 0;
            }
        }

        characterController.Move(new Vector3(0, -fallSpeed) * Time.deltaTime);
    }
    void IsGrounded()
    {
        isGrounded = (Physics.Raycast(transform.position, -transform.up, characterController.height * 1.1F));
    }

    void IsGroundedInWall ()
    {
        isGroundedInWall = (Physics.Raycast(transform.position, transform.right, characterController.radius * 1.2F));
    }
}
