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
    public void PlaySpecificSoundVolume(int ValueTableau2)
    {
        AudioSourceEnfant.clip = EnfantTableau[ValueTableau2];
        AudioSourceEnfant.volume = 0.05f;
        AudioSourceEnfant.Play();
        isPlayingBetter = true;

    }


    public void PlayFallDownTreeSound()
    {
        AudioSourceEnfant.clip = EnfantTableau[0];
        AudioSourceEnfant.Play();
        AudioSourceEnfant.volume = 1f;

    }
    public void PlayScreamSound()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[11];
        AudioSourceEnfant.Play();

    }
    public void PlayFallDownSound()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[12];
        AudioSourceEnfant.Play();

    }
    public void PlayHealSound()
    {
            AudioSourceEnfant.volume = 1f;
            AudioSourceEnfant.clip = EnfantTableau[1];
            AudioSourceEnfant.Play();
            
    }
    public void PlayWoodSound()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[21];
        AudioSourceEnfant.Play();

    }
    public void PlayWoodSound2()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[23];
        AudioSourceEnfant.Play();

    }

    public void PlayFarSound()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[22];
        AudioSourceEnfant.Play();
        

    }
    public void PlayColdSound()
    {
        AudioSourceEnfant.volume = 1f;
        AudioSourceEnfant.clip = EnfantTableau[15];
        AudioSourceEnfant.Play();
        

    }
    public void PlayHotSound()
    {

        AudioSourceEnfant.clip = EnfantTableau[17];
        AudioSourceEnfant.Play();
        AudioSourceEnfant.volume = 0.1f;


    }

}
