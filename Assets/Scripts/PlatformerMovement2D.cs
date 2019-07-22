using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerMovement2D : MonoBehaviour {

    [Header("Movement")]
    public float moveSpeed = 7f;
    public float maxSpeed = 20f;
    [Header("Jumping")]
    public int numJumps = 1;
    public float jumpForce = 10f;
    [Range(0f, 1f)] public float moveStopTime = 0.5f;
    public LayerMask groundLayer;

    private Rigidbody2D rigid;
    private Collider2D col;
    private float horizontal;
    private bool isGrounded = false;
    private int jumpsRemaining;

	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
	}

    private void FixedUpdate() {
        if (horizontal < 0) {
            if (rigid.velocity.x < maxSpeed)
                rigid.velocity += Vector2.left * moveSpeed * Time.fixedDeltaTime;
        } else if (horizontal > 0) {
            if (rigid.velocity.x < maxSpeed)
                rigid.velocity += Vector2.right * moveSpeed * Time.fixedDeltaTime;
        } else {
            rigid.velocity = Vector2.Lerp(rigid.velocity, 
                new Vector2(0, rigid.velocity.y), moveStopTime);
        }
    }

    void Update () {
        horizontal = Input.GetAxis("Horizontal");
        IsGrounded();
        if (isGrounded) {
            jumpsRemaining = numJumps;
        }
        if (Input.GetButtonDown("Jump")) {
            if (jumpsRemaining > 0) {
                Jump();
            }
        }
	}

    void Jump() {
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsRemaining--;
        //isGrounded = false;
    }

    void IsGrounded() {
        Vector2 boxCenter = (Vector2)col.bounds.center - new Vector2(0, col.bounds.extents.y);
        Vector2 boxSize = new Vector2(col.bounds.max.x * 2, 0.15f);
        Debug.DrawLine(boxCenter, boxCenter + Vector2.down * 0.15f, Color.magenta);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(boxCenter, boxSize, 0, groundLayer);
        int counter = colliders.Where(c => groundLayer == 
            (groundLayer | (1 << c.gameObject.layer))).Count();
        isGrounded = counter > 0;
    }
}
