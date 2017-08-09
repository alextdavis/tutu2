using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public enum Action
	{
		Idle,
		Walking,
		Jumping,
		Falling,
	}

	public Action currentAction = Action.Idle;

	Rigidbody2D rb;

	public LayerMask Ground;

	bool faceRight = false;
	bool grounded = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		Running();
	}

	void FixedUpdate() {
		CheckGround();
	}

	void CheckGround() {
		if (rb.velocity.y > -0.1 && rb.velocity.y < 0.1) {
			Vector2 footPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(0.0f, -0.5f);
			RaycastHit2D hit = Physics2D.Raycast(footPos, Vector2.down, 0.1f, Ground);
			if (hit) {
				grounded = true;
			} else {
				grounded = false;
			}
		}
		//print("Grounded: " + grounded);
	}

	void CheckTurn() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, Ground);
	}

	void Running() {
		if (faceRight && grounded) {
			//increase velocity x
			rb.velocity = new Vector2(2.0f, rb.velocity.y);
		} else {
			//decrease velocity x
			rb.velocity = new Vector2(-2.0f, rb.velocity.y);
		}
	}
}
