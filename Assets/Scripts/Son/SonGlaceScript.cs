using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonGlaceScript : MonoBehaviour
{
    public AudioSource AudioSourceGlace;

    // Start is called before the first frame update


    void Start()
    {
        AudioSourceGlace = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<iceScript>().meltTimer <= 0)
        {
            AudioSourceGlace.Play();
            print("laglacefond");
        }
    }
    public void IceMelting()
    {
        AudioSourceGlace.Play();
        
    }
        

}   
