using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayerCamera : MonoBehaviour
{
    public LayerMask groundAndSpikesLayer;
    [SerializeField] public float raycastLength;
    [SerializeField] private float spherecastRadius;
    [SerializeField] private Vector2 raycastOffset;
    public int spikeDamage = 1;
    public PlayerMovement playerMovement;
    private bool isRaycastActive = true;


    private bool isGrounded;

    public bool IsGrounded
    {
        get { return isGrounded; }
        private set { isGrounded = value; }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        CheckGround();
        if (GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            DeactivateRaycast();
        }
        else
        {
            ActivateRaycast();
        }
    }


    private void CheckGround()
    {
        Vector2 raycastOrigin = (Vector2)transform.position + raycastOffset;
        Vector2 raycastDirection = Vector2.down;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(raycastOrigin, spherecastRadius, raycastDirection, raycastLength, groundAndSpikesLayer);

        Debug.DrawRay(raycastOrigin, raycastDirection * raycastLength, isGrounded ? Color.red : Color.green);

        int segments = 20;
        float angleIncrement = 180f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -180f + i * angleIncrement;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * spherecastRadius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * spherecastRadius;

            Vector3 pointOnCircle = raycastOrigin + new Vector2(x, y);

            Debug.DrawLine(raycastOrigin, pointOnCircle, isGrounded ? Color.red : Color.green);
        }

    }
    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.red : Color.green;

        Vector3 rayStart = transform.position + new Vector3(raycastOffset.x, raycastOffset.y, 0);
        Gizmos.DrawWireSphere(rayStart, spherecastRadius);

        int segments = 20;
        float angleIncrement = 180f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = -180f + i * angleIncrement;
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * spherecastRadius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * spherecastRadius;

            Vector3 pointOnCircle = rayStart + new Vector3(x, y, 0);

            Gizmos.DrawLine(rayStart, pointOnCircle);
        }
    }
    private void ActivateRaycast()
    {
        isRaycastActive = true;
    }
    private void DeactivateRaycast()
    {
        isRaycastActive = false;
    }
    private bool IsGoingUpwards()
    {
        return GetComponent<Rigidbody2D>().velocity.y > 0;
    }
}
