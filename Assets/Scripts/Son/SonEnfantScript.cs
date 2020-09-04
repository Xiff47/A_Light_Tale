using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonEnfantScript : MonoBehaviour
{
    public AudioClip[] EnfantTableau;
    AudioSource AudioSourceEnfant;
    
    public bool isPlayingBetter;

    // Start is called before the first frame update
    void Start()
    {
        AudioSourceEnfant = gameObject.GetComponent<AudioSource>();
        // kidScript = GetComponent<kidScript>;
        isPlayingBetter = false;
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void PlaySpecificSound(int ValueTableau)
    {
        AudioSourceEnfant.clip = EnfantTableau[ValueTableau];
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.Play();
        isPlayingBetter = true;

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
   

}
