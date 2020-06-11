using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonEnfantScript : MonoBehaviour
{
    public AudioClip[] EnfantTableau;
    AudioSource AudioSourceEnfant;
    public bool isIndoor;

    // Start is called before the first frame update
    void Start()
    {
        AudioSourceEnfant = gameObject.GetComponent<AudioSource>();
       // kidScript = GetComponent<kidScript>;
    }

    // Update is called once per frame
    void Update()
    {
      

    }
    public void PlayFallDownTreeSound()
    {
        AudioSourceEnfant.clip = EnfantTableau[0];
        AudioSourceEnfant.Play();
        
    }
    public void PlayScreamSound()
    {
        AudioSourceEnfant.clip = EnfantTableau[12];
        AudioSourceEnfant.Play();

    }
    public void PlayFallDownSound()
    {
        AudioSourceEnfant.clip = EnfantTableau[13];
        AudioSourceEnfant.Play();

    }
    public void PlayHealSound()
    {
        
            AudioSourceEnfant.clip = EnfantTableau[1];
            AudioSourceEnfant.Play();
            
    }
    public void PlayStepSound()
    {
        if(isIndoor)
            {
            AudioSourceEnfant.clip = EnfantTableau[Random.Range(7, 12)];
            AudioSourceEnfant.pitch = (Random.Range(0.6f, 1.2f));
            AudioSourceEnfant.Play();
            print("footstepIndoor");
            
            }
        else
        {
            {
                AudioSourceEnfant.clip = EnfantTableau[Random.Range(3, 6)];
                AudioSourceEnfant.Play();
                AudioSourceEnfant.pitch = (Random.Range(0.6f, 1.2f));
                print("footstepOutdoor");
            }
        }
    }

}
