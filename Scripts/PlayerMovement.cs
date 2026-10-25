using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool hasLanded = false;
    private LavaRise lava;
    private float lavaStartDelay = 3f;
    private float lavaStartTimer = 0f;
    private bool timerStarted = false;
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 14f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpBufferTimer;
    private float jumpBufferTime = 0.15f;
    private bool jumpBuffered;

    [HideInInspector] public bool jumpBoosted;
    [HideInInspector] public float jumpBoostMultiplier = 1.5f;

    void Start()
    {
        lava = FindObjectOfType<LavaRise>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBuffered = true;
            jumpBufferTimer = jumpBufferTime;
        }

        if (jumpBufferTimer > 0) jumpBufferTimer -= Time.deltaTime;
        else jumpBuffered = false;

        if (jumpBuffered && isGrounded)
        {
            float force = jumpBoosted ? jumpForce * jumpBoostMultiplier : jumpForce;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
            jumpBuffered = false;
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayJump();
        }

        // Start timer when player first jumps
        if (!timerStarted && !isGrounded)
        {
            timerStarted = true;
        }

        // Start lava 3 seconds after first jump
        if (timerStarted && !hasLanded)
        {
            lavaStartTimer += Time.deltaTime;
            if (lavaStartTimer >= lavaStartDelay)
            {
                hasLanded = true;
                if (lava != null) lava.StartRising();
            }
        }

        WrapHorizontal();
    }

    void WrapHorizontal()
    {
        float hw = Camera.main.orthographicSize * Camera.main.aspect;
        if (transform.position.x > hw + 0.5f)
            transform.position = new Vector3(-hw - 0.4f, transform.position.y, 0);
        else if (transform.position.x < -hw - 0.5f)
            transform.position = new Vector3(hw + 0.4f, transform.position.y, 0);
    }
}