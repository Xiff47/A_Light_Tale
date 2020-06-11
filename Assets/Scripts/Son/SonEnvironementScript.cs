using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonEnvironementScript : MonoBehaviour
{
	bool isPlaying = false;
	bool hasPlayed = false;
    AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && !audioSource.isPlaying){
			hasPlayed = true;
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag=="Player" && !hasPlayed){
			isPlaying = true;
            audioSource.Play();
            print("music");

			//play
		}
	}
	//void OnTriggerExit2D(Collider2D other)
    //{
		//if(other.gameObject.tag=="Kid"){
			//stop
}
	

