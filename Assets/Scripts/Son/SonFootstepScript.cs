using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFootstepScript : MonoBehaviour
{
    public AudioClip[] FootstepTableau;
    AudioSource AudioSourceFootstep;
    public bool isIndoor;

    void Start()
    {
        isIndoor = false;
    }

    public void PlayStepSound()
    {
        if (isIndoor)
        {
            AudioSourceFootstep.clip = FootstepTableau[Random.Range(4, 8)];
            AudioSourceFootstep.pitch = (Random.Range(0.6f, 1.2f));
            AudioSourceFootstep.Play();
            print("footstepIndoor");

        }
        else
        {
            {
                AudioSourceFootstep.clip = FootstepTableau[Random.Range(1, 4)];
                AudioSourceFootstep.volume = (0.1f);
                AudioSourceFootstep.Play();
                AudioSourceFootstep.pitch = (Random.Range(0.6f, 1.2f));
                print("footstepoutdoor");

            }
        }
    }
}
