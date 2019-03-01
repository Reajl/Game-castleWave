using UnityEngine;
using System.Collections;

public class move : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float playerSpeed;
    public float jumpPower;
    public int directionInput;
    public bool groundCheck;
    public bool facingRight = true;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        if ((directionInput < 0) && (facingRight))
        {
            Flip();
        }

        if ((directionInput > 0) && (!facingRight))
        {
            Flip();
        }
        groundCheck = true;
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(playerSpeed * directionInput, rb2d.velocity.y);
    }

    public void Move(int InputAxis)
    {

        directionInput = InputAxis;

    }

    public void Jump(bool isJump)
    {
        isJump = groundCheck;

        if (groundCheck)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpPower);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


}
