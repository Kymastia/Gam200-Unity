using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Logic")]
    [SerializeField] private Animator anim;
    private CharacterController controller;
    //For going down 
    private Vector3 slopeNormal;
    //Check if the player is grounded
    private bool grounded;
    private float verticalVelocity;


    [Header("Movement")]
    [SerializeField] private float speedX = 5;
    [SerializeField] private float speedZ = 5;

    //How fast the player will fall if jumping, incrementally
    [SerializeField] private float gravity = 0.25f;

    //For if we are using Jumping
    [SerializeField] private float jumpForce = 8.0f;

    //The max speed you can fall
    [SerializeField] private float terminalVelocity = 5.0f;
    public Rigidbody rb;

    [Header("GroundCheckRayCast")]
    //Determines how the ground is being checked
    [SerializeField] private float extremitiesOffset = 0.05f;
    [SerializeField] private float innerVerticalOffset = 0.25f;
    [SerializeField] private float distanceGrounded = 0.15f;
    [SerializeField] private float slopeThreshold = 0.55f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

    }

    private void FixedUpdate()
    {
        Vector3 inputVector = PoolInput();
        Move(inputVector);

    }

    private Vector3 PoolInput()
    {
        Vector3 r = default(Vector3);
        r.x = Input.GetAxisRaw("Horizontal");
        r.z = Input.GetAxisRaw("Vertical");
        return r.normalized;
    }

    private void Move(Vector3 direction)
    {
        float moveX = direction.x * speedX * Time.deltaTime;
        float moveZ = direction.z * speedZ * Time.deltaTime;
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        rb.MovePosition(rb.position + movement);
    }

    public bool Grounded()
    {

        //If the character is jumping or falling
        if (verticalVelocity < 0)
            return false;
        else return true;
        /*
        float yRay = (controller.bounds.center.y - (controller.height) * 0.5f) + innerVerticalOffset;
        RaycastHit hit;

            Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime;
    transform.Translate(movement);
        */

    }
}
