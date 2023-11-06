using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spike enemyPrefab; // The enemy prefab
    public int numberOfEnemies = 10; // The number of enemies to spawn at the start of each level
    public float spawnInterval = .5f; // The time interval between each spawn
    private List<string> enemiesDestroyed; // List to track which enemies have been destroyed

    Dictionary<GameObject, string> enemyIDs = new Dictionary<GameObject, string>();

    void Start()
    {
        // Initialize the list
        enemiesDestroyed = new List<string>(new string[numberOfEnemies]);

        // Start the coroutines for each row
        for (int j = 0; j < 2; j++)
        {
            StartCoroutine(SpawnEnemies(j));
        }
    }

    public void ResetLevel()
    {
        // Clear the enemiesDestroyed list
        enemiesDestroyed.Clear();

        // Stop all SpawnEnemies coroutines
        StopAllCoroutines();

        // Start the SpawnEnemies coroutines again
        for (int j = 0; j < 2; j++)
        {
            StartCoroutine(SpawnEnemies(j));
        }
    }

    IEnumerator SpawnEnemies(int row)
    {
        // Get the world coordinates of the left and right of the screen
        float left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float top = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float bottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Calculate the height of the top third of the screen
        float topThirdHeight = (top - bottom) / 3;

        // Calculate the height of one row within the top third
        float rowHeight = topThirdHeight / 3;

        // Calculate the y coordinate for this row
        float spawnY = top - topThirdHeight + rowHeight * (row + 1);

        while (true)
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                // If this enemy has been destroyed, skip to the next one
                string id = row + "-" + i;
                if (enemiesDestroyed.Contains(id))
                {
                    Debug.Log("Skipping enemy " + id);
                }
                else
                {
                    // Instantiate an enemy off the left side of the screen
                    Vector3 spawnPosition = new Vector3(left - 1, spawnY, 0);
                    Spike enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                    // Assign the index to the enemy
                    enemy.index = i;
                    enemy.gameManager = this;
                    enemyIDs[enemy.gameObject] = id;
                }

                // Wait for the next spawn
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        // Get the ID of this enemy
        string id = enemyIDs[enemy];

        // Mark this enemy as destroyed
        enemiesDestroyed.Add(id);

        // If all enemies are destroyed, reset the level
        if (enemiesDestroyed.Count == numberOfEnemies * 2) // Multiply by 2 because there are 2 rows of enemies
        {
            ResetLevel();
        }
    }
}
