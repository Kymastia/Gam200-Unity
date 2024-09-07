using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
