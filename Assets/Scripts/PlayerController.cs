using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField] int jumpForce = 10;
    [SerializeField] int moveSpeed = 100;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 10.2f;
    
    bool isGrounded;
    [SerializeField] LayerMask groundLayers;

    Vector2 workspace = Vector2.zero;

    private float xInput = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
       ApplyMovementVelocity();
       isGrounded = IsOnGround();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.started && isGrounded)
        {
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<Vector2>().x;
    }

    void ApplyMovementVelocity() 
    {
        workspace.x = xInput * moveSpeed;
        workspace.y = rb2d.velocity.y;
        rb2d.velocity = workspace;
    }

    bool IsOnGround()
    {
        return (Physics2D.Raycast(groundCheck.position, Vector3.down, 1f, groundLayers));
    }
}
