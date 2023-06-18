using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovenment : MonoBehaviour
{

    private float playerSpeed = 5.5f;
    private float jumpStrength = 8f;

    private float horizontalMovement;


    bool facingRight;
    bool jumping = false;
    float jumpCount = 0;

    private Rigidbody2D playerRigidBody;

    Vector3 velocityZero = Vector3.zero;

    void Awake()
    {

        // Assigns player rigid body to RigidBody2d of component when loaded

        playerRigidBody = GetComponent<Rigidbody2D>();

        //creates safety check if playerRigidBody is not found
        if (!playerRigidBody.TryGetComponent(out Rigidbody2D playerRigidbody))
        {
            Debug.LogError("Rigidbody2D component not found on the PlayerMovement script's GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Horizontal movenment will be equal to negative or positive playerspeed
        horizontalMovement = Input.GetAxisRaw("Horizontal") * playerSpeed;
        
        //Checks for player jump which is used by the move character function
        if (Input.GetButtonDown("Jump"))
      {
            jumping = true;
        }
    }

    void FixedUpdate()
    {

        moveCharacter(horizontalMovement, jumping);
        if (playerRigidBody.velocity.y < 0)
        {
            //if player is falling, increase gravity scale
            playerRigidBody.gravityScale = 4f;

        } else if (playerRigidBody.velocity.y <= 0)
        {
            playerRigidBody.gravityScale = 2.5f;

        }

        //always set jumping to false after character moves
        jumping = false;
    }

    //Allows player to flip directions
    public void Flip(float horizontalMovement)
    {
        facingRight = horizontalMovement > 0;
        Vector3 theScale = transform.localScale;
        theScale.x = Mathf.Abs(theScale.x) * (facingRight ? 1 : -1);
        transform.localScale = theScale;
    }

    public void moveCharacter(float moveSpeed, bool jump)
    {

        //player's velocity is a vector according to the player horizontal and vertical velocity
        Vector3 targetVelocity = new Vector2(moveSpeed, playerRigidBody.velocity.y);
        playerRigidBody.velocity = Vector3.SmoothDamp(playerRigidBody.velocity, targetVelocity, ref velocityZero, 0.05f);

        //if jumping is true, adds upward vector
        if (jump && jumpCount<2)
        {
            playerRigidBody.AddForce(new Vector2(0f, jumpStrength), ForceMode2D.Impulse);
        }

    }
}
