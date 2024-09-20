using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public Vector3 positionOffset = Vector3.zero;
    [SerializeField] public int yOffset = 0;
    [SerializeField] public int xOffset = 0;
    private int damageValue = 1;
    public Rigidbody2D rb;
    [SerializeField] public int flySide;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(flySide, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<PlayerMovement>().LoseHealth(damageValue);
            Destroy(gameObject);
        }
    }

}