using UnityEngine;

//This script controls the bulk of what the player does, including its movement and physics.
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("A reference to the bike model")]
    public Transform bike;

    [Tooltip("How quickly the bike accelerates forward.")]
    public float forwardAcceleration = 0.5f;
    [Tooltip("How quickly the bike accelerates backward.")]
    public float backwardAcceleration = 0.1f;
    [Tooltip("The maximum speed of the bike.")]
    public float maxVelocity = 10f;
    [Tooltip("How quickly you can turn left and right.")]
    public float turnStrength = 90f;
    [Tooltip("How much friction occurs while the player is on the ground. A higher number means the player slows down more when stopping.")]
    public float dragOnGround = 3f;
    
    [Tooltip("The angle that the bike leans when turning.")]
    public float maxBikeLean = 5f;

    // variables that alter the physics (rigidbody) of the player
    private Rigidbody body;
    
    // variable for determining whether the player is on the ground or not
    private bool isGrounded = true;
    
    // Start is called at the beginning of the game
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //updates the turnInput variable when player is trying to move left or right
        float turnInput = Input.GetAxis("Horizontal");

        //controls the rotation of the player in reference to the terrain while the player is on the ground
        if (isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        // tilts the bike if we are turning
        bike.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, -turnInput * maxBikeLean);
    }

    private void FixedUpdate()
    {
        //resets speed of the player at start of each frame
        float speedInput = 0f;

        //moves player forward based on its acceleration in that direction
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAcceleration * 1000f;
        }

        //moves player backwards based on its acceleration in that direction
        else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * backwardAcceleration * 1000f;
        }

        //if player is on the ground, then rigidbody will be updated with the player's inputs
        if(isGrounded)
        {
            body.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                body.AddForce(transform.forward * speedInput);
            }
        }
        else
        {
            //if player isn't on the ground, then rigidbody is updated to have no drag and to begin returning back to the ground
            body.drag = 0.1f;
        }
        
        // makes sure that the player isn't moving too fast
        body.velocity = Vector3.ClampMagnitude(body.velocity, maxVelocity);
    }

    // If we are touching the ground, mark that we are on the ground
    public void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        transform.rotation = Quaternion.FromToRotation(transform.up, other.impulse) * transform.rotation;
    }

    // When we stop touching the ground, mark that we are not on the ground
    public void OnCollisionExit()
    {
        isGrounded = false;
    }
}
