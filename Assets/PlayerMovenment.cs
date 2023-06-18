using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovenment : MonoBehaviour
{

    private float playerSpeed = 5.5f;
    private float jumpStrength = 8f;

    bool facingRight;
    bool jumping;
    float horizontalMovenment;
    private Rigidbody2D playerRigidBody;

    Vector3 velocityZero = Vector3.zero;

    // Assigns player rigid body to RigidBody2d of component when loaded
    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovenment = Input.GetAxisRaw("Horizontal") * playerSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }

        


    }

    void FixedUpdate()
    {
        //flips the character accordingly
        Flip(horizontalMovenment);

        moveCharacter(horizontalMovenment * Time.fixedDeltaTime, jumping);

    }

    //Allows player to flip directions
    public void Flip(float horizontalMovenment)
    {
        if (horizontalMovenment == 1)
        {
            facingRight = true;
        } else if(horizontalMovenment == -1)
        {
            facingRight = false;
        }

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void moveCharacter(float moveSpeed, bool jump)
    {

        //player's velocity is a vector according to the player horizontal and vertical velocity
        Vector3 targetVelocity = new Vector2(moveSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = Vector3.SmoothDamp(playerRigidBody.velocity, targetVelocity, ref velocityZero, 0.05f);

        if(jump)
        {
            playerRigidBody.AddForce(new Vector2(0f, jumpStrength));
        }

    }
}
