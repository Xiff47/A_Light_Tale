using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFeuScript : MonoBehaviour
{
    public AudioClip[] FeuTableau;
    AudioSource AudioSourceFeu;
    public bool startingFire;

    // Start is called before the first frame update
    void Start()
    {
        startingFire = false;
        AudioSourceFeu = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startingFire){
            if (!AudioSourceFeu.isPlaying){
                startingFire = false;
				print("fin allumage");
				loopFireSound();
            }
        }
    }
	
	public void playLightUpSound(){
		AudioSourceFeu.clip = FeuTableau[0];
        AudioSourceFeu.Play();
		startingFire = true;
		print("allumage !");
	}
	
	void loopFireSound(){
		AudioSourceFeu.loop = true;
		AudioSourceFeu.clip = FeuTableau[1];
        AudioSourceFeu.Play();
        print("sonfeuestjoué");
	}

}
    
  
    

