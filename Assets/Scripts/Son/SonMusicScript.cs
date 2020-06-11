using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMusicScript : MonoBehaviour
{
    public AudioClip[] musicTableau;
    AudioSource AudioSourceMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioSourceMusic = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
