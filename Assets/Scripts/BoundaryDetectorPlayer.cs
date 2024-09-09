using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryDetectorPlayer : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] public float leftRaycastLength;
    [SerializeField] public float rightRaycastLength;
    [SerializeField] public float frontRaycastLength;
    [SerializeField] public float backRaycastLength;

    [SerializeField] public Vector3 leftRaycastOffset;
    [SerializeField] public Vector3 rightRaycastOffset;
    [SerializeField] public Vector3 frontRaycastOffset;
    [SerializeField] public Vector3 backRaycastOffset;

    [SerializeField] public LayerMask boundary;

    public bool canMoveLeft{ get; private set; }
    public bool canMoveRight { get; private set; }
    public bool canMoveFront { get; private set; }
    public bool canMoveBack { get; private set; }

    void Update()
    {
        TouchingBoundaries();
    }

    private void TouchingBoundaries()
    {
        //Left
        if (Physics.Raycast(transform.position + leftRaycastOffset, Vector3.left, leftRaycastLength, boundary))
        {
            canMoveLeft = false;
            Debug.DrawRay(transform.position + leftRaycastOffset, Vector3.left * leftRaycastLength, Color.red);
        }
        else
        {
            canMoveLeft = true;
            Debug.DrawRay(transform.position + leftRaycastOffset, Vector3.left * leftRaycastLength, Color.green);
        }

        //Right
        if (Physics.Raycast(transform.position + rightRaycastOffset, Vector3.right, rightRaycastLength, boundary))
        {
            canMoveRight = false;
            Debug.DrawRay(transform.position + rightRaycastOffset, Vector3.right * rightRaycastLength, Color.red);
        }
        else

        {
            canMoveRight = true;
            Debug.DrawRay(transform.position + rightRaycastOffset, Vector3.right * rightRaycastLength, Color.green);
        }

        //Front
        if (Physics.Raycast(transform.position + frontRaycastOffset, Vector3.back, frontRaycastLength, boundary))
        {
            canMoveFront = false;
            Debug.DrawRay(transform.position + frontRaycastOffset, Vector3.back * frontRaycastLength, Color.red);
        }
        else
        {
            canMoveFront = true;
            Debug.DrawRay(transform.position + frontRaycastOffset, Vector3.back * frontRaycastLength, Color.green);
        }

        //Back
        if (Physics.Raycast(transform.position + backRaycastOffset, Vector3.forward, backRaycastLength, boundary))
        {
            canMoveBack = false;
            Debug.DrawRay(transform.position + backRaycastOffset, Vector3.forward * backRaycastLength, Color.red);
        }
        else
        {
            canMoveBack = true;
            Debug.DrawRay(transform.position + backRaycastOffset, Vector3.forward * backRaycastLength, Color.green);
        }
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + leftRaycastOffset, Vector3.left * leftRaycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + rightRaycastOffset, Vector3.right * rightRaycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + frontRaycastOffset, Vector3.back * frontRaycastLength);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + backRaycastOffset, Vector3.forward * backRaycastLength);
    }
}
