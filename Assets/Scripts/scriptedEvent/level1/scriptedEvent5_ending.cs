using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptedEvent5_ending : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
	[SerializeField] GameObject light;
	[SerializeField] GameObject kid;
	[SerializeField] GameObject canvas;
	[SerializeField] GameObject teleportDestination;
	[SerializeField] public float timer = 10;
	[SerializeField] Text textEndGame;
	byte colorAlpha;
	
	bool start = false;
	bool happened = false;
	bool fade = false;
	
	float t;
	
	int step = 0;
	bool done = false;
	
	float targetDistance;
	
    // Start is called before the first frame update
    void Start()
    {
		t = timer;
		colorAlpha = 0;
		textEndGame.color = new Color32(0, 0, 0, colorAlpha);
    }

    // Update is called once per frame
    void Update()
    {
		/*
		* 0- make weather worse and begin overlay apparition
		* 1- Once overlay appeared block everyting wait a few second
		* 2- teleport and make overlay desappear
		* 3- once overlay desappeared, text appears.
		*/
	
		if(!start || (happened && !fade)){
			return;
		}
		
		switch(step){
			case 0 : 
				if(!done){
					// worsen weather
					gameObject.GetComponent<scriptedEvent4_snowStance>().SetHeavySnow();
					done = true;
				}
				float visibility = 1-(t/timer);
				canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, visibility);
				t -= Time.deltaTime;
				if(t<=0){
					timer = 3;
					t = timer;
					stepUp();
				}
			break;
			case 1 :
				if(!done){
					// block everything
					light.GetComponent<lightScript>().toggleActive();
					light.GetComponent<lightScript>().acceptGivre = false;
					kid.GetComponent<kidScript>().Die();
					done = true;
				}
				t -= Time.deltaTime;
				if(t<=0){
					timer = 3;
					t = timer;
					stepUp();
				}
			break;
			case 2 :
				if(!done){
					// teleport
					light.transform.position = teleportDestination.transform.position;
					done = true;
				}
				float visibility2 = t/timer;
				canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, visibility2);
				t -= Time.deltaTime;
				if(t<=0){
					timer = 3;
					t = timer;
					stepUp();
				}
			break;
			case 3 :
				if(!done){
					// appear text
					done = true;
					happened = true;
					fade = true;
				}
			break;
			
		}

		// Texte apparait en fondu
		if(happened && fade && colorAlpha<254){
			colorAlpha += 2;
			textEndGame.color = new Color32(0, 0, 0, colorAlpha);
			textEndGame.gameObject.SetActive(true);
		}else{
			fade = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			start = true;
		}
	}
	
	public void activate(){
		start = true;
	}
	
	void stepUp(){
		step += 1;
		done = false;
	}
}
