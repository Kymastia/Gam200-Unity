using UnityEngine;

public class CannonBallScript : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] public int flySide;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(flySide, 0, 0 );
    }
}