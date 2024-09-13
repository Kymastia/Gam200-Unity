using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidanceScript : MonoBehaviour
{
    public float avoidDistance = 2f;  // Distance to check for nearby enemies
    public float avoidStrength = 3f;  // Force with which the enemies push away from each other
    public string enemyTag = "Enemy";  // Tag used to identify other enemies

    private EnemyMovement enemyMovement;  // Reference to the existing movement script

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();  // Get the movement script on the enemy
    }

    private void Update()
    {
        AvoidOtherEnemies();  // Check for other enemies and avoid them
    }

    private void AvoidOtherEnemies()
    {
        // Find all enemies with the specified tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject otherEnemy in enemies)
        {
            // Skip self
            if (otherEnemy == this.gameObject) continue;

            // Calculate distance between this enemy and the other enemy
            float distance = Vector3.Distance(transform.position, otherEnemy.transform.position);

            // If within the avoidance distance, push away from each other
            if (distance < avoidDistance)
            {
                Vector3 directionAway = (transform.position - otherEnemy.transform.position).normalized;
                transform.position += directionAway * avoidStrength * Time.deltaTime;
            }
        }
    }
}
