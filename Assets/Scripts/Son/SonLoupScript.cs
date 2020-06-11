using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonLoupScript : MonoBehaviour
{
    public AudioClip[] loupTableau;
    public AudioSource AudioSourceLoup;

    // Start is called before the first frame update
    void Start()
    {
        AudioSourceLoup = gameObject.GetComponent<AudioSource>();
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

    }
    public void PlayGrogneSound()
    {
        AudioSourceLoup.clip = loupTableau [2];
        AudioSourceLoup.Play();
        print("grognesound");
    }
    public void PlayGoodBoySound()
    {
        AudioSourceLoup.clip = loupTableau[1];
        AudioSourceLoup.Play();
        print("secalme");

    }
}
