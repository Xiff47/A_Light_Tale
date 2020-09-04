using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonLoupScript : MonoBehaviour
{
    public AudioClip[] loupTableau;
    public AudioSource AudioSourceLoup;
    public bool grogning;

    // Start is called before the first frame update
    void Start()
    {
        AudioSourceLoup = gameObject.GetComponent<AudioSource>();
        grogning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBarkSound()
    {
        AudioSourceLoup.clip = loupTableau[0];
        AudioSourceLoup.Play();
        print("aboie");
        grogning = false;

    }
    public void PlayGrogneSound()
    {
        if (grogning == false)
        {
            AudioSourceLoup.clip = loupTableau[2];
            AudioSourceLoup.Play();
            print("grognesound");
            grogning = true;
        }
    }
    public void PlayGoodBoySound()
    {
        AudioSourceLoup.clip = loupTableau[1];
        AudioSourceLoup.Play();
        print("secalme");
        grogning = false;

    }
}
