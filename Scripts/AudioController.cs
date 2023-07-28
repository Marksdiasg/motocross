using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the sfx for the motorcycles engine revving.

//REQUIREMENTS: This script can be placed on the Player GameObject, but will work from anywhere. This script requires there to be an
//AudioListener in the scene as well as an SFX to play.
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;   //Audio Source component attached to GameObject
    public float minVolume = 0.05f, maxVolume = 0.15f;  //Minimum & Maximum Volume for Audio Source

    // Update is called once per frame
    void Update()
    {
        //Controls volume of the audio source based on whether the player is moving or not
        audioSource.volume = Mathf.Clamp(Mathf.Abs(Input.GetAxis("Vertical")), minVolume, maxVolume);
    }
}
