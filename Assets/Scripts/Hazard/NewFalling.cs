using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewFalling : MonoBehaviour
{
    /*
        Currently, FallingObjectSpawner, spawns the falling object
        Falling Object has a script that spawns the warning
        Warning moves itself to a specific Y coordinate based on camera and at the same X axis
        The warning dies upon being touched by the object

     */

    /* 
     * New Plan
     * FallingObjectSpawner, spawns the warning
     * The warning is attached to a specific Y coordinate based on camera and at the same X axis
     * The warning plays an animation
     * The warning spawns the object at the same X coordinate but pushed upward by an offset in Y coordinate
     * It dies upon being touched by the object
     * Do a white to red tone
     */

    [SerializeField] public float yOffset = 0f;
    [SerializeField] private float customScaleX = 1f;
    [SerializeField] private float customScaleY = 1f;


    private void Update()
    {
        Camera mainCamera = Camera.main;

        //Variable newPosition is set to the current position which is that of the camera
        Vector3 newPosition = transform.position;

        //Set the Y position to this one
        newPosition.y = mainCamera.transform.position.y + yOffset;

        //Position set to newPosition
        transform.position = newPosition;

        //Change size to dictated one
        transform.localScale = new Vector3(customScaleX, customScaleY, 1f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CannonBall"))
        {
            Destroy(gameObject);
        }
    }
}
