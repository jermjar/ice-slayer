using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 7f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpForce = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;
    
    [Header("Health")]
    public HeartSystem heart;
    public bool isImmune;
    public float immunityTime;
    public float immunityDuration = 2f;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;

    [Header("Collisions")]
    public bool onGround;
    public float groundLength = 0.6f;
    public Transform raycastSpawn;
    public Vector3 colliderOffset;
    public float xRange = 9.85f;

    void Awake()
    {
    }

    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        onGround = Physics2D.Raycast(raycastSpawn.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(raycastSpawn.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if (isImmune == true)
        {
            immunityTime += Time.deltaTime;
            if(immunityTime >= immunityDuration)
            {
                isImmune = false;
                immunityTime = 0;
            }

        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        moveCharacter(direction.x);

        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }

        modifyPhysics();
    }

    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if((horizontal > 0 && !facingRight || horizontal < 0 && facingRight))
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }

        if (onGround)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastSpawn.position + colliderOffset, raycastSpawn.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(raycastSpawn.position - colliderOffset, raycastSpawn.position - colliderOffset + Vector3.down * groundLength);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (((other.gameObject.CompareTag("HitBox") || other.gameObject.CompareTag("Boss")) || other.gameObject.CompareTag("Icicle")) && (isImmune == false))
        {
            heart.TakeDamage(-1);
            isImmune = true;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        //audioSource.PlayOneShot(clip);
    }
}
