using System;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] int moveSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;

    public PlayerInputActions playerControls;
    Vector2 moveDirection = Vector2.zero;

    InputAction movement;
    InputAction jumpAction;

    float jumpPower = 15f;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerControls = new PlayerInputActions();
    }

    void InputActions()
    {
        movement = playerControls.Player.Move;
        jumpAction = playerControls.Player.Jump;
        movement.performed += Move;
        jumpAction.performed += Jump;
    }

    void OnEnable()
    {
        InputActions();
        movement.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        jumpAction.Disable();
    }

    void Update()
    {
        moveDirection = movement.ReadValue<Vector2>().normalized;
        Vector2 currentVelocity = rb.linearVelocity;
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, currentVelocity.y);
    }

    void FixedUpdate()
    {
        CheckIfGrounded();
    }

    void Move(InputAction.CallbackContext context)
    {
        Debug.Log("There's movement");
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            Debug.Log("Jumping");
        }
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            movement.Disable();
        }
        else
        {
            movement.Enable();
        }

    }
}