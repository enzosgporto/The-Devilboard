using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{

    private Rigidbody2D MonsterBody;

    float initialSpeed = 2f;
    Vector3 velocityZero = Vector3.zero;
    
    void Awake()
    {
        MonsterBody = GetComponent<Rigidbody2D>();

        //creates safety check if rigidBody is not found
        if (!MonsterBody.TryGetComponent(out Rigidbody2D playerRigidbody))
        {
            Debug.LogError("Rigidbody2D component not found on the PlayerMovement script's GameObject.");
        }
    }

    void FixedUpdate()
    {
        float speed = initialSpeed * Time.deltaTime;
        Vector3 targetVelocity = new Vector2(speed, MonsterBody.velocity.y);
        MonsterBody.velocity = Vector3.SmoothDamp(MonsterBody.velocity, targetVelocity, ref velocityZero, 0.05f);
    }
}
