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
    [SerializeField] int bulletSpeed;

    public PlayerInputActions playerControls;
    Vector2 moveDirection = Vector2.zero;

    InputAction movement;
    InputAction jumpAction;
    InputAction fireAction;
    InputAction altFireAction;

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
        fireAction.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        jumpAction.Disable();
        fireAction.Disable();
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

    /*
    void Fire(InputAction.CallbackContext context)
    {
        Command fireCommand = new FireCommand(rb, bulletSpeed, moveDirection, BulletType.Normal);
        fireCommand.Execute();
    }
    /*
    */

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

    void TakeDamage(int damage)
    {

        health -= damage;
        if (health < 0) health = 0;

        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void Die()
    {
        Debug.Log("Player is dead");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(20);
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