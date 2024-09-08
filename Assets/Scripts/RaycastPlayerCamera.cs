using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayerCamera : MonoBehaviour
{
    //This code is for the Raycast that will be used
    //To check if there is a collider at the edge of the screen
    //If so, it will set a flag that tells the camera to stop tracking the player
    //Upon leaving, it will reset the flag

    [SerializeField] public float leftRaycastLength;
    [SerializeField] public float rightRaycastLength;
    [SerializeField] public float leftRaycastOffset;
    [SerializeField] public float rightRaycastOffset;
    [SerializeField]  public Vector3 raycastOffset = new Vector3(0, 0, 0);
    [SerializeField] public LayerMask boundary;

    private bool isTouchingBoundaryLeft = false;
    private bool isTouchingBoundaryRight = false;
    public bool isTouchingBoundary { get; private set; }

    private void Start()
    {
    }
    private void Update()
    {
        
        CheckSides();

    }

    private void CheckSides()
    {
        //        Vector3 leftRayCastOrigin = transform.position;

        //Origin,Direction,MaxDistance,LayerMask,QueryTriggerInteraction
        if (Physics.Raycast(transform.position + raycastOffset, Vector3.left, leftRaycastLength, boundary))
        {
            isTouchingBoundaryLeft = true;
            Debug.DrawRay(transform.position + raycastOffset, Vector3.left * leftRaycastLength, Color.red);
        }
        else
        //Sets the flag to false and changes the color to green
        {
            isTouchingBoundaryLeft = false;
            Debug.DrawRay(transform.position + raycastOffset, Vector3.left * leftRaycastLength, Color.green);
        }

        if (Physics.Raycast(transform.position + raycastOffset, Vector3.right, rightRaycastLength, boundary))
        {
            isTouchingBoundaryRight = true;
            Debug.DrawRay(transform.position + raycastOffset, Vector3.right * rightRaycastLength, Color.red);
        }
        else
        {
            isTouchingBoundaryRight = false;
            Debug.DrawRay(transform.position + raycastOffset, Vector3.right * rightRaycastLength, Color.green);
        }
        //if either side detects a boundary set it to true

        if (isTouchingBoundaryLeft || isTouchingBoundaryRight)
        {
            isTouchingBoundary = true;
        }
        else
        {
            isTouchingBoundary = false;
        }
    }
    // Visualize the raycast in the Scene View
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + raycastOffset, Vector3.left * leftRaycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + raycastOffset, Vector3.right * rightRaycastLength);
    }

    /*   


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
        }*/
}
