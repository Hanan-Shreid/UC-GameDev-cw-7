using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;

    public Transform groundCheck;

    public float rad;

    public LayerMask ground;

    public bool isGrounded;

    public float jumpForce;

    bool facingRight = true;

    public float bulletSpeed;

    SpriteRenderer sprite;

    Animator anim;

    string currentAnim;

    public GameObject bullet;

    const string IDLE_ANIM = "idle";
    const string WALK_ANIM = "walk";
    const string JUMP_ANIM = "Jump";

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }


    void Update()
    {
        Shoot();
    }

    void PlayerMove()
    {
        float xPos = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(xPos, rb.velocity.y);
        Flip();

        if (isGrounded && xPos == 0 && rb.velocity.y == 0)
        {
            PlayAnim(IDLE_ANIM);
        }
        else if (isGrounded && xPos != 0 && rb.velocity.y == 0)
        {
            PlayAnim(WALK_ANIM);
        }
    }
    void PlayerJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);

        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            PlayAnim(JUMP_ANIM);
        }

    }

    void Flip()
    {
        if (Input.GetKey(KeyCode.D) && !facingRight)
        {
            sprite.flipX = false;
            facingRight = true;
        }
        else if (Input.GetKey(KeyCode.A) && facingRight)
        {
            sprite.flipX = true;
            facingRight = false;
        }
    }

    void PlayAnim(string toPlay)
    {
        if(currentAnim == toPlay)
        {
            return;
        }
        currentAnim = toPlay;
        anim.Play(toPlay);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);

        if(facingRight)
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);
            }
        else
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0f);
            }

            Destroy(bulletClone, 2.5f);
        }

    }


}
