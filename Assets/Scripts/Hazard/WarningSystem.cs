using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarningSystem : MonoBehaviour
{
    [SerializeField] public float yOffset = 0f;
    [SerializeField] private float customScaleX = 1f;
    [SerializeField] private float customScaleY = 1f;

    public GameObject BGPrefab;
    private SpriteRenderer spriteRenderer;
    public float lerpDuration = 3f; 
    public float targetRedValue = 2f;



    private void Start()
    {
        SpawnBGObject();

    }
    private void Update()
    {
    Camera mainCamera = Camera.main;
    Vector3 newPosition = transform.position;
    newPosition.y = mainCamera.transform.position.y + yOffset;
    transform.position = newPosition;
    transform.localScale = new Vector3(customScaleX, customScaleY, customScaleY);

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
       if (other.CompareTag("FlowerPot"))
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

