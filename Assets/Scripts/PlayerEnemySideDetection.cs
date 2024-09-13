using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemySideDetection : MonoBehaviour
{
    //Send a raycast from both ends, circle?
    // Start is called before the first frame update
    [Header("Raycast")]
    [SerializeField] public Vector3 leftRaycastLength;
    [SerializeField] public Vector3 rightRaycastLength;
    [SerializeField] public Vector3 leftRaycastOffset;
    [SerializeField] public Vector3 rightRaycastOffset;
    [SerializeField] public bool leftOccupied;
    [SerializeField] public bool rightOccupied;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckRight();
        CheckLeft();
    }

    private void CheckRight()
    {
        rightOccupied = false;
        Vector3 rayCastOrigin = (Vector3)transform.position + rightRaycastOffset;
        Vector3 raycastDirection = Vector3.right;
        RaycastHit[] rightHits = Physics.BoxCastAll(rayCastOrigin, rightRaycastLength, raycastDirection);

        foreach (var hit in rightHits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                rightOccupied = true;
                break;
            }

        }
    }
    private void CheckLeft()
    {
        leftOccupied = false;
        Vector3 rayCastOrigin = (Vector3)transform.position + leftRaycastOffset;
        Vector3 raycastDirection = Vector3.left;
        RaycastHit[] leftHits = Physics.BoxCastAll(rayCastOrigin, leftRaycastLength, raycastDirection);

        foreach (var hit in leftHits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                leftOccupied = true;
                break;
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = rightOccupied ? Color.red : Color.green;

        Vector3 rightRayStart = transform.position + rightRaycastOffset;
        Gizmos.DrawWireCube(rightRayStart, rightRaycastLength);

        Gizmos.color = leftOccupied ? Color.red : Color.green;

        Vector3 leftRayStart = transform.position + leftRaycastOffset;
        Gizmos.DrawWireCube(leftRayStart, leftRaycastLength);
    }
        
    }
