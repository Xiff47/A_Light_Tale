using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonGlaceScript : MonoBehaviour
{
    public AudioSource AudioSourceGlace;
    bool isplaying;

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
            isplaying = false;
            print("fond");
            if (isplaying == false)
            {
                AudioSourceGlace.Play();
                print("laglacefond");
                isplaying = true;


            }
        }
    }
    public void IceMelting()
    {
        AudioSourceGlace.Play();
        
    }
        

}   
