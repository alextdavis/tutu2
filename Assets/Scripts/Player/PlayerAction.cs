using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public enum Action
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Throwing,
        Spinning
    }

    public Action CurrentAction = Action.Idle;
	
	 private Rigidbody2D rb;

	 public LayerMask Ground;

	 private bool grounded = false;

    // Use this for initialization
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
        CheckAction();
    }

    private void FixedUpdate()
    {
        
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(2, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-2, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, 3));
        }
    }

    private void CheckAction()
    {
        CurrentAction = Action.Idle;
        if (rb.velocity.x > 0.1 && rb.velocity.x < -0.1)
        {
            CurrentAction = Action.Walking;
        }

        if (rb.velocity.y > 0.1)
        {
            CurrentAction = Action.Jumping;
        } else if (rb.velocity.y < 0.1)
        {
            CurrentAction = Action.Falling;
        }
    }

	void CheckGround()
	{
		if (rb.velocity.y > -0.1 && rb.velocity.y < 0.1)
		{
			Vector2 footPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0.0f, -0.5f);
			RaycastHit2D hit = Physics2D.Raycast(footPos, Vector2.down, 0.1f, Ground);
			if (hit)
			{
				grounded = true;
			}
			else
			{
				grounded = false;
			}
		}
		//print("Grounded: " + grounded);
	}
}
