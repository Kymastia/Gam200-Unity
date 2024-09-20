using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CannonBallWarningSystem : MonoBehaviour
{
    //This should be linked to the camera's X axis.
    //This should be the warning
    //It should also handle the CannonBall Spawning?

    [SerializeField] public float xOffset = 0f;
    private GameObject BGPrefab;
    private SpriteRenderer spriteRenderer;
    public float lerpDuration = 3f;
    public float targetRedValue = 2f;

    public float targetValue = 0f;
    private Color initialColor;


    private void Start()
    {
        //Spawn the background, probably spawn a logo
        //Make it spawn Warning Additional part
        //SpawnBGObject();

    }
    private void Update()
    {

        Camera mainCamera = Camera.main;
        Vector3 newPosition = transform.position;
        newPosition.x = mainCamera.transform.position.x + xOffset;
        transform.position = newPosition;
        //Get the renderer, set the initialColor to the current Color
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        //Start the coroutine to change the color
        //Slowly Turn the stuff Red
        StartCoroutine(LerpColor());
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CannonBallWarningSystem: I'm Triggering");
        if (other.CompareTag("CannonBall"))
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeImage(float targetAlpha)
    {
        Color startColor = initialColor;

        float currentTime = 0;
        float duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            /*float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, currentTime / duration);*/
            float newAlpha = 0f;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            spriteRenderer.color = newColor;
            Debug.Log(newAlpha);
            newAlpha += 0.2f;
            yield return null;
        }

        Color finalColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        spriteRenderer.color = finalColor;
    }
    public IEnumerator LerpColor()
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
    }
}
/*
public IEnumerator BlinkIcon()
{
    if (isFading)
    {
        yield break;
    }

    isFading = true;

    yield return StartCoroutine(FadeImage(0f));

    yield return StartCoroutine(FadeImage(1f));

    isFading = false;
}
*/