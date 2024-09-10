using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    [SerializeField] public float speed;
    public float largeXBuffer = 3f;  // Large buffer for initial X alignment
    public float smallXBuffer = 0.5f;  // Small buffer for final X alignment
    public float stopDistance = 0.5f;


    private bool hasReachedZ = false;  // Flag to check if the enemy has aligned with the player on the Z-axis
    private bool hasAlignedX = false;  // Flag to check if the enemy has aligned with the player on the X-axis

    public bool isPlayerToLeft { get; private set; }
    public bool isPlayerToRight { get; private set; }

    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 position = transform.position;
        Vector3 targetPosition = player.position;

        // Calculate the distance between the enemy and player on the X and Z axes
        float distanceX = targetPosition.x - position.x;
        float distanceZ = targetPosition.z - position.z;

        // Update the booleans based on whether the enemy is to the left or right of the player
        isPlayerToLeft = distanceX > 0;
        isPlayerToRight = distanceX < 0;

        // Phase 1: Move diagonally towards the player (X and Z simultaneously)
        if (!hasReachedZ || !hasAlignedX)
        {
            float moveX = Mathf.Sign(distanceX) * speed * Time.deltaTime;
            float moveZ = Mathf.Sign(distanceZ) * speed * Time.deltaTime;

            // Move towards player's Z position without any buffer
            if (Mathf.Abs(distanceZ) > stopDistance)
            {
                position.z += moveZ;
            }
            else
            {
                hasReachedZ = true;  // Mark Z alignment when close enough to the player
            }

            // Move towards player's X position but stop at a large buffer
            if (Mathf.Abs(distanceX) > largeXBuffer && !hasAlignedX)
            {
                position.x += moveX;
            }
            else if (Mathf.Abs(distanceX) <= largeXBuffer && !hasAlignedX)
            {
                hasAlignedX = true;  // Mark X alignment when within the large buffer
            }
        }

        // Phase 2: Once Z alignment is done, adjust X to close the gap within the small buffer
        if (hasReachedZ && hasAlignedX && Mathf.Abs(distanceX) > smallXBuffer)
        {
            float moveX = Mathf.Sign(distanceX) * speed * Time.deltaTime;
            position.x += moveX;
        }

        // Stop moving if within the small X buffer and near the player's Z position
        if (hasReachedZ && Mathf.Abs(distanceX) <= smallXBuffer && Mathf.Abs(distanceZ) <= stopDistance)
        {
            hasAlignedX = true;
        }

        // Phase 3: Reactivate movement if the player moves beyond the stop distance on X or Z
        if (Mathf.Abs(distanceX) > smallXBuffer || Mathf.Abs(distanceZ) > stopDistance)
        {
            hasReachedZ = false;
            hasAlignedX = false;
        }

        // Update the enemy's position
        transform.position = position;
    }

}
