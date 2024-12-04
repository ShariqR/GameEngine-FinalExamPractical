using System;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : Singleton<MonoBehaviour>
{
    Rigidbody2D rb;
    [SerializeField] int moveSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;

    public PlayerInputActions playerControls;
    Vector2 moveDirection = Vector2.zero;

    InputAction movement;
    InputAction jump;

    public static PlayerController Instance {get; private set;}

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
        jump = playerControls.Player.Jump;
        movement.performed += Move;
        jump.performed += Jump;
    }

    void OnEnable()
    {
        InputActions();
        movement.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        jump.Disable();
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
        if (collision.gameObject.CompareTag("Snowflake"))
        {
            movement.Disable();
        }
        else
        {
            movement.Enable();
        }

    }
}