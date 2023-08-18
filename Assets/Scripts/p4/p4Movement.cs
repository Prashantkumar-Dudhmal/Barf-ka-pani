using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p4Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private GameManager gameManager;

    [SerializeField] private LayerMask jumpableGround;

    private enum movementState { idle, running, jumping, falling, freezed }
    movementState state;

    //private bool isFreezed4 = false;
    [SerializeField] private float moveX;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 16f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        while (gameManager.isGameActive)
        {
            moveX = Input.GetAxisRaw("Vertical1");
            rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.Keypad8) && IsOnGround())
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            AnimationStateUpdate();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trampoline"))
            rb.velocity = new Vector2(rb.velocity.x, 29f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            rb.bodyType = RigidbodyType2D.Static;
            state = movementState.freezed;
            gameManager.isFreezed4 = true;
            gameManager.freezedCount++;
        }
        else if (collision.gameObject.tag == "Player" && gameManager.isFreezed4 == true)
        {
            StartCoroutine(Wait5Sec());
        }
        anim.SetBool("isFreezed", gameManager.isFreezed4);
    }
    IEnumerator Wait5Sec()
    {
        yield return new WaitForSeconds(1);
        rb.bodyType = RigidbodyType2D.Dynamic;
        state = movementState.idle;
        gameManager.isFreezed4 = false;
        gameManager.freezedCount--;

    }

    private void AnimationStateUpdate()
    {
        
        if (rb.velocity.x < -0.1f)
        {
            state = movementState.running;
            sprite.flipX = true;
        }
        else if (rb.velocity.x > 0.1f)
        {
            state = movementState.running;
            sprite.flipX = false;
        }
        else
            state = movementState.idle;

        if (rb.velocity.y < -0.1f)
        {
            state = movementState.falling;
        }
        else if (rb.velocity.y > 0.1f)
        {
            state = movementState.jumping;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool IsOnGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
