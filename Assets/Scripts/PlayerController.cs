using System;
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
    private int maxjumps = 2;
    private int jumpsRemaining;

    [SerializeField] LayerMask groundLayers;

    Vector2 workspace = Vector2.zero;

    Vector2 lookDirection;

    private float xInput = 0;

    private int nutCount;
    
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxjumps;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var nut = col.gameObject.GetComponent<NutController>();
        if (nut != null)
        {
            nut.PickUp();
            nutCount++;
            Debug.Log(nutCount);
        }
    }

    void Update()
    {
        if (IsOnGround() && rb2d.velocity.y <= 0)
        {
            jumpsRemaining = maxjumps;
        }
        CheckWhereToFace ();
    }

    void FixedUpdate()
    {
        ApplyMovementVelocity();
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && jumpsRemaining > 0)
        {
            jump();
        }
    }

    private void jump()
    {
        workspace.x = rb2d.velocity.x;
        workspace.y = 0;
        rb2d.velocity = workspace;
        rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    public void Move(InputAction.CallbackContext context)
    {
        var input = context.ReadValue<Vector2>();
        xInput = input.x;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        //if (context.started) 
        {
            Debug.Log("Interact is hit");
            IfInRangeOfNpc();
            
        }
    }


    void IfInRangeOfNpc()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb2d.position + Vector2.up * 0.2f, lookDirection, 15f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NpcController character = hit.collider.GetComponent<NpcController>();
                character.DisplayDialog();
            }
    }

    void ApplyMovementVelocity()
    {
        workspace.x = xInput * moveSpeed;
        workspace.y = rb2d.velocity.y;
        rb2d.velocity = workspace;
        
    }

    bool IsOnGround()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayers);
    }

    void CheckWhereToFace ()
	{
		if (xInput > 0 && !spriteRenderer.flipX)
        {
			FlipSprite();
            lookDirection = new Vector2(1,0);
        }
		else if (xInput < 0 && spriteRenderer.flipX )
        {
            FlipSprite();		
            lookDirection = new Vector2(-1,0);
        }

	}


    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
