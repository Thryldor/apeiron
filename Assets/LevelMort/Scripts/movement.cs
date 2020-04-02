using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float fallMultiplier = 200.5f;
    public float lowJumpMultiplier = 200.0f;
    private Rigidbody2D rb;
    public Camera mc;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * 5;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.position += Vector3.left * Time.deltaTime * 5;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rb.velocity = Vector2.up*7;
        }
       /* if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        } else*/ /*if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
        }*/
    }
    
}
