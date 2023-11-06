using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the spike
    public GameManager gameManager; // Reference to the GameManager
    public int index; // Index of this enemy in the row
    private bool wasHitByBullet; // Flag to indicate whether the enemy was hit by a bullet


    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy was hit by a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Collision of " + gameObject.name + " with " + collision.gameObject.name);
            // Set the flag
            wasHitByBullet = true;

            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the spike to the right
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

        // Get the world coordinates of the left and right of the screen
        // float left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - 1;
        float right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        // If the enemy has moved off the right side of the screen, destroy it
        if (transform.position.x > right + 1)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // If the enemy was hit by a bullet, notify the GameManager
        if (wasHitByBullet)
        {
            Debug.Log("Set enemy destroyed at index " + index);
            gameManager.EnemyDestroyed(this.gameObject);
        }
    }
}
