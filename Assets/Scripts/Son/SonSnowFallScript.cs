using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonSnowFallScript : MonoBehaviour
{
    AudioSource audioSourceSnowFall;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceSnowFall = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySnowFallSound()
    {
       
        audioSourceSnowFall.Play();
        print("laneigetombe");
        
    }
}
