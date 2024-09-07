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
    [SerializeField] private float speedY = 5;

    //How fast the player will fall if jumping, incrementally
    [SerializeField] private float gravity = 0.25f;

    //For if we are using Jumping
    [SerializeField] private float jumpForce = 8.0f;

    //The max speed you can fall
    [SerializeField] private float terminalVelocity = 5.0f;

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

    private void Update()
    {
        Vector3 inputVector = PoolInput();
    }

    private Vector3 PoolInput()
    {
        Vector3 r = default(Vector3);

        r.x = Input.GetAxisRaw("Horizontal");
        r.z = Input.GetAxisRaw("Vertical");
        return r.normalized;
    }
}
