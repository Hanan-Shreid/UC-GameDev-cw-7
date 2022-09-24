using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    public float rad;

    public Transform groundCheck;
    public LayerMask ground;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }



    void Patrol()
    {
        bool IsGrounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);
        rb.velocity = new Vector2(speed, 0f);

        if (!isGrounded)
        {
            speed *= -1;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            SceneManager.LoadScene(0);
        }
    }
}