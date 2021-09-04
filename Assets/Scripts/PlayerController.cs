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

    private int nutCount;

    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var nut = col.gameObject.GetComponent<NutController>();
        if(nut != null)
        {
            nut.PickUp();
            nutCount++;
            Debug.Log(nutCount);
        }
    }



    void Update()
    {
    }

    void FixedUpdate()
    {
       ApplyMovementVelocity();
       CheckWhereToFace ();
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
        var input = context.ReadValue<Vector2>();
        xInput = input.x;
    }

    void ApplyMovementVelocity() 
    {
        workspace.x = xInput * moveSpeed;
        workspace.y = rb2d.velocity.y;
        rb2d.velocity = workspace;
        
    }

    bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector3.down, 1f, groundLayers);
    }

    void CheckWhereToFace ()
	{
		if (xInput > 0 && spriteRenderer.flipX)
			FlipSprite();
		else if (xInput < 0 && !spriteRenderer.flipX )
			FlipSprite();		
	}


    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
