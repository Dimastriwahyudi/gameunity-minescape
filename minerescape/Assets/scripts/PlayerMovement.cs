using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private PlayerController playerController;

    private Vector2 moveInput;
    private bool isJumping = false;

    private enum MovementState { idle, jump, fall, run }

    [Header("Jump Settings")]
    [SerializeField] private LayerMask jumpableGround;
    private BoxCollider2D coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();

        playerController = new PlayerController(); // pastikan kamu sudah generate PlayerController dari Input Actions
    }

    private void OnEnable()
    {
        playerController.Enable();

        playerController.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerController.Movement.Move.canceled += ctx => moveInput = Vector2.zero;

        playerController.Movement.Jump.performed += ctx => Jump();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    // ===>> Start() dari NewBehaviourScript
    void Start()
    {
        Debug.Log("Start method dari NewBehaviourScript dijalankan");
    }

    private void Update()
    {
        moveInput = playerController.Movement.Move.ReadValue<Vector2>();

        // Tambahan Update dari NewBehaviourScript bisa ditaruh di sini
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state;

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            state = MovementState.run;
            sprite.flipX = moveInput.x < 0;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.fall;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
