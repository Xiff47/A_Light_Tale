using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFootstepScript : MonoBehaviour
{
    public AudioClip[] FootstepTableau;
    AudioSource AudioSourceFootstep;

    void Start()
    {
        AudioSourceFootstep = gameObject.GetComponent<AudioSource>();
    }

    public void PlayStepSound()
    {
        if (transform.parent.GetComponent<kidScript>().isInWater)
        {
            AudioSourceFootstep.clip = FootstepTableau[12];
        }
        else if (transform.parent.GetComponent<kidScript>().isInBlizzard)
        {
            AudioSourceFootstep.clip = FootstepTableau[Random.Range(9, 11)];
        }
        else if (transform.parent.GetComponent<kidScript>().isIndoor)
        {
            AudioSourceFootstep.clip = FootstepTableau[Random.Range(4, 8)];
        }
        else
        {
            AudioSourceFootstep.clip = FootstepTableau[Random.Range(1, 4)];
        }

        AudioSourceFootstep.volume = (0.1f);
        AudioSourceFootstep.Play();
        AudioSourceFootstep.pitch = (Random.Range(0.6f, 1.2f));
    }
}
