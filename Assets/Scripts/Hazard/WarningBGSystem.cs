using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningBGSystem : MonoBehaviour
{
    //Don't Use
    [SerializeField] public float xOffset = 0f;
    public GameObject CannonBall;
    private SpriteRenderer spriteRenderer;
    public float lerpDuration = 3f;
    public float targetValue = 0f;
    private Color initialColor;



    private void Start()
    {
        //Get the renderer, set the initialColor to the current Color
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        //Start the coroutine to change the color
        StartCoroutine(LerpColor());
    }
    private void Update()
    {
        Camera mainCamera = Camera.main;
        Vector3 newPosition = transform.position;
        newPosition.x = mainCamera.transform.position.x + xOffset;
        transform.position = newPosition;
    }


    IEnumerator LerpColor()
    {
        float timeElapsed = 0f;
        Color currentColor = initialColor;

        while (timeElapsed < lerpDuration)
        {
            // Calculate the interpolation factor
            float t = timeElapsed / lerpDuration;

            // Lerp the red value
            float lerpedValue = Mathf.Lerp(currentColor.r, targetValue, t);

            // Update the color with the new red value
            Color newColor = new Color(currentColor.r, lerpedValue, lerpedValue, currentColor.a);
            spriteRenderer.color = newColor;

            // Increment timeElapsed
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        // Ensure we set the color to the exact target color at the end of the lerp
        spriteRenderer.color = new Color(currentColor.r, 0, 0, currentColor.a);
        SpawnFallingObject();
    }
    private void SpawnFallingObject()
    {

        GameObject cannonBall = Instantiate(CannonBall);

        Camera mainCamera = Camera.main;

        //Vector3 adjustedPosition = new Vector3(transform.position.x, mainCamera.transform.position.y + fallingYOffset, 0f);

        //cannonBall.transform.position = adjustedPosition;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CannonBall"))
        {
            Destroy(gameObject);
        }
    }

}

