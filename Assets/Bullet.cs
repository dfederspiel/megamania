using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f; // Speed of the bullet
    public float lifespan = 2.0f; // The lifespan of the bullet in seconds
    

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMove = speed * Time.deltaTime;
        float horizontalMove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(horizontalMove, verticalMove, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
