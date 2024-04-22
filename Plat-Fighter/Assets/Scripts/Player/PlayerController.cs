using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.2f; // Coyote time duration
    public float doubleJumpForce = 8f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField]
    private bool isGrounded = false;
    private bool canDoubleJump = false;
    private float coyoteTimer = 0f;
    private PlayerInput controls;
    public GameObject weapon;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        controls = new PlayerInput();
        controls.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Jump.performed += Jump;
        controls.Player.Look.performed += Aim;
    }

    private void OnDisable()
    {
        controls.Player.Jump.performed -= Jump;
        controls.Player.Look.performed -= Aim;
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.01f, groundMask);

        // Reset double jump ability if grounded
        if (isGrounded) canDoubleJump = true;

        // Coyote time logic
        coyoteTimer -= Time.deltaTime;

        // Movement input
        Vector2 moveInput = controls.Player.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // transform.localScale = new Vector3(moveInput.x > 0f ? 1.0f : -1.0f, transform.localScale.y, transform.localScale.z);
    }

    void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded || coyoteTimer > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimer = coyoteTime;
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
            canDoubleJump = false;
        }
        // Debug.Log(rb.velocity);
    }

    void Aim(InputAction.CallbackContext context)
    {
        // Debug.Log(context.control.device.ToString());
        // Check which control scheme is active
        if (context.control.device.ToString() == "Mouse:/Mouse" && controls.Player.Look.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionToMouse = worldMousePosition - transform.position;
            directionToMouse.Normalize();

            float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
            if (90.0f < angle && angle < 180.0f)
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, -1.0f, 1.0f);
            }
            else if (-180.0 < angle && angle < -90.0f)
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, -1.0f, 1.0f);
            }
            else
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, 1.0f, 1.0f);
            }

            // Apply rotation to GameObject around z-axis
            Debug.Log(angle);
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (context.control.device.ToString() == "DualSenseGamepadHID:/DualSenseGamepadHID" && controls.Player.Look.ReadValue<Vector2>() != Vector2.zero)
        {
            // Controller input detected
            Vector2 controllerInput = controls.Player.Look.ReadValue<Vector2>();
            Vector2 normInput = controllerInput.normalized;
            Debug.Log(normInput);

            float angle = Mathf.Atan2(normInput.y, normInput.x) * Mathf.Rad2Deg;
            if (90.0f < angle && angle < 180.0f)
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, -1.0f, 1.0f);
            }
            else if (-180.0 < angle && angle < -90.0f)
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, -1.0f, 1.0f);
            }
            else
            {
                weapon.transform.localScale = new Vector3(transform.localScale.x, 1.0f, 1.0f);
            }

            // Apply rotation to GameObject around z-axis
            weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }  
    }
    
    // void OnCollisionStay2D(Collision2D collision)
    // {
    //     // Prevent getting stuck on edges
    //     foreach (ContactPoint2D contact in collision.contacts)
    //     {
    //         if (contact.normal.y > 0.5f)
    //         {
    //             rb.velocity = new Vector2(rb.velocity.x, 0);
    //         }
    //     }
    // }
}
