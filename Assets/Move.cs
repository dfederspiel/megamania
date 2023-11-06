using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public float speed = 15.0f; // Speed of the player movement
    public Bullet bulletPrefab; // The bullet prefab
    public float spawnRate = .25f; // The rate at which bullets can be spawned
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        // Get the screen boundaries
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + 1;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - 1;

        // Clamp the player's x position to the screen boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, screenLeft, screenRight);

        transform.position = newPosition;

        if (Input.GetKey(KeyCode.Space) && Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0); // Add an offset to the y position
            Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
