using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{

    private Rigidbody2D monstersBody;

    float initialSpeed = 2f;
    Vector3 velocityZero = Vector3.zero;
    
    void Awake()
    {
        monstersBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float speed = initialSpeed * Time.deltaTime;
        Vector3 targetVelocity = new Vector2(speed, 0f);
        monstersBody.velocity = Vector3.SmoothDamp(monstersBody.velocity, targetVelocity, ref velocityZero, 0.05f);
    }
}
