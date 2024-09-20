using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarningSystem : MonoBehaviour
{
    //This should be linked to the camera's X axis.
    //This should be the warning

    [SerializeField] public float xOffset = 0f;    
    private GameObject BGPrefab;
    private SpriteRenderer spriteRenderer;
    public float lerpDuration = 3f; 
    public float targetRedValue = 2f;



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

    }

    private void SpawnBGObject()
    {

        GameObject Background = Instantiate(BGPrefab);

        Camera mainCamera = Camera.main;

        Vector3 adjustedPosition = new Vector3(transform.position.x, transform.position.y, 1000f);

        Background.transform.position = adjustedPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.CompareTag("CannonBall"))
        {
            Destroy(gameObject);
        }
    }

/*    public IEnumerator BlinkIcon()
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
    public IEnumerator FadeImage(float targetAlpha)
    {
        Color startColor = spriteR.color;

        float currentTime = 0;
        float duration = 0.5f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            *//*float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, currentTime / duration);*//*
            float newAlpha = 0;
            Color newColor = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            spriteR.color = newColor;
            Debug.Log(newAlpha);
            newAlpha += 0.2f;
            yield return null;
        }

        Color finalColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        spriteR.color = finalColor;
    }*/
}

