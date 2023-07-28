using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("The game object that should be enabled when the player enters this waypoint's area. This can be another waypoint, a win screen, or anything else.")]
    public GameObject enableOnCollision;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            // Disable this waypoint
            gameObject.SetActive(false);
            
            // If we have a game object that we want to enable...
            if (enableOnCollision != null)
            {
                // ... then enable it!
                enableOnCollision.SetActive(true);
            }
        }
    }
}