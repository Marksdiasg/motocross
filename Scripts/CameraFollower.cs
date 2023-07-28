using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Tooltip("The player's game object, or whatever game object we want to follow.")]
    public GameObject follow;
    
    public float speed = 5;

    Vector3 offset;

    void Start()
    {
        offset = follow.transform.position - transform.position;
    }

    void FixedUpdate()
    {
        // Look
        var newRotation = Quaternion.LookRotation(follow.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);

        // Move
        Vector3 forward = follow.transform.forward;
        forward.y = 0;
        Vector3 newPosition = follow.transform.position - forward * offset.z - follow.transform.up * offset.y;
        transform.position = Vector3.Slerp(transform.position, newPosition, Time.fixedDeltaTime * speed);
    }
}