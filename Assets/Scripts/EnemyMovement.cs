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

    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("State")]
    public bool isActive = false; // Checks if the player has anyone occupying
    public bool idleDirection = false;

    PlayerEnemySideDetection playerEnemySideDetection;

    void Update()
    {
        checkPlayer();
        if (isActive)
        {
            MoveTowardsPlayer();
            FlipCharacter();
        }
        else
        {
            Idle();
        }

    }

    private void checkPlayer()
    {
        if (playerEnemySideDetection.leftOccupied && playerEnemySideDetection.rightOccupied)
        {
            isActive = false;
        }
        else
        {
            isActive = true;
        }
    }
    private void Idle()
    {
        Vector3 position = transform.position;
        int randomDistance;
        if (idleDirection == true)
        {
            randomDistance = Random.Range(3, 5);
            spriteRenderer.flipX = true;
        }
        else
        {
            randomDistance = Random.Range(-3, -5);
            spriteRenderer.flipX = false;
        }

        Vector3 targetPosition = transform.position;
        targetPosition.x += randomDistance;




    }

    private void FlipCharacter()
    {
        if ( isPlayerToRight)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        //THe enemy's current position
        Vector3 position = transform.position;
        //The player position
        Vector3 targetPosition = player.position;

        // The distance between the player and the enemy for both axis
        float distanceX = targetPosition.x - position.x;
        float distanceZ = targetPosition.z - position.z;

        // Sets the boolean to true for the enemy being on whatever side, this is for helping 
        // the enemy sprite decide which way to face, or which sprite to use, yada yada
        // for the enemy clumping one, probably use some raycast on the player end
        isPlayerToLeft = distanceX > 0;
        isPlayerToRight = distanceX < 0;

        // Stage 1, If the character is neither aligned nor reached the point, do that
        if (!hasReachedZ || !hasAlignedX)
        {
            float moveX = Mathf.Sign(distanceX) * speed * Time.deltaTime;
            float moveZ = Mathf.Sign(distanceZ) * speed * Time.deltaTime;

            // Align the enemy to the player on Z axis
            if (Mathf.Abs(distanceZ) > stopDistance && !hasReachedZ)
            {
                position.z += moveZ;
            }
            else if (position.z == targetPosition.z)
            {
                hasReachedZ = true; //This only gets turned off if the player goes past a threshold
            }

            // Move towards player's X position but stop with a large buffer
            // Gives the player an idea that the enemy is approaching
            if (Mathf.Abs(distanceX) > largeXBuffer && !hasAlignedX)
            {
                position.x += moveX;
            }

            else if (Mathf.Abs(distanceX) <= largeXBuffer && !hasAlignedX)
            {
                hasAlignedX = true;  // This only gets turned off if the player leaves the threshold
            }
        }

        // Stage 2, Now that player aligned and close on X, move closer
        // Has fulfilled the Z axis, gotten within X distance
        if (hasReachedZ && hasAlignedX && Mathf.Abs(distanceX) > smallXBuffer)
        {
            // Move horizontally
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
