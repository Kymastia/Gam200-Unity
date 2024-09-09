using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
public class PlayerMovement : MonoBehaviour
{
    //some things to think about, if the player is in the air how slow should their movement be?
    [Header("Logic")]
    [SerializeField] private Animator anim;
    private CharacterController controller;

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

    public BoundaryDetectorPlayer boundaryDetectorPlayer;


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
        if (r.x < 0 && !boundaryDetectorPlayer.canMoveLeft) 
        {
            r.x = 0;
        }
        if (r.x > 0 && !boundaryDetectorPlayer.canMoveRight) 
        {
            r.x = 0;
        }

        //Positive means going back
        if (r.z < 0 && !boundaryDetectorPlayer.canMoveFront)
        {
            r.z = 0;
        }
        if (r.z > 0 && !boundaryDetectorPlayer.canMoveBack)
        {
            r.z = 0;
        }

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
