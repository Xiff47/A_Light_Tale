using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptedEvent5_ending_v2 : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
	[SerializeField] GameObject light;
	[SerializeField] GameObject kid;
	[SerializeField] GameObject wood;
	[SerializeField] GameObject kidPositionNearFire;
	[SerializeField] GameObject fire;
	[SerializeField] GameObject canvas;
	[SerializeField] GameObject lightDestination;
	[SerializeField] public float timer = 3;
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
			step 1:
			-Kid moves to position (near fire)
			once done: step++
			step 2:
			-Kid waits 3sec
			once done: step++
			step 3:
			-Kid moves to position (near wood)
			once done: simulate mouse1 & step++
			step 4:
			-Kid moves to position (near fire)
			once done: launch fire lightning animation :point_left:  Someone need to create the animation
			once done & fire lightning animation finished: step++
			step 5:
			launch fire happy animation :point_left:  Someone need to create the animation
			launch 5sec timer
			Once timer done: making screen white & finish game
		*/
	
		if(!start || (happened && !fade)){
			return;
		}
		switch (step){
			case 0 : // kid goes to fireplace
				if(!done){
					kid.GetComponent<kidScript>().SpecialFollow(kidPositionNearFire.transform.position);
					light.GetComponent<lightScript>().SpecialFollow(lightDestination.transform.position);
					light.GetComponent<lightScript>().toggleActive();
					light.GetComponent<lightScript>().acceptGivre = false;
					done = true;
				}
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - kidPositionNearFire.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - kidPositionNearFire.transform.position.y),2)) <= 4){
					stepUp();
				}
			break;
			case 1 : // kid studies fireplace
				t -= Time.deltaTime;
				if(t<=0){
					timer = 2;
					t = timer;
					stepUp();
				}
			break;
			case 2 :// kid goes to wood and pick it up
				if(!done){
					kid.GetComponent<kidScript>().SpecialFollow(wood.transform.position);
					done = true;
				}
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - wood.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - wood.transform.position.y),2)) <= 1){
					kid.GetComponent<kidScript>().CheckItem();
					stepUp();
					t = 1.5f;
				}
			break;
			case 3:
				t -= Time.deltaTime;

				if (t <= 0)
				{
					timer = 2;
					t = timer;
					stepUp();
				}
				break;
			case 4 : //kid returns to fire and depose wood
				if(!done){
					kid.GetComponent<kidScript>().SpecialFollow(kidPositionNearFire.transform.position);
					done = true;
				}
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - kidPositionNearFire.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - kidPositionNearFire.transform.position.y),2)) <= 4){
					kid.GetComponent<kidScript>().CheckItem();
					stepUp();
				}
			break;
			case 5 : //kid lights fire
				if(!done){
					// YVAN DO YOUR MAGIC
					done = true;
				}
				if(false){ // when anim finished YVAN DO YOUR MAGIC
					fire.GetComponent<firePlaceScript>().LightUp();
					stepUp();
				}
			break;
			case 6 : //light dances
				if(!done){
					// YVAN DO YOUR MAGIC
					done = true;
				}
				t -= Time.deltaTime;
				if(t<=0){
					timer = 5;
					t = timer;
					stepUp();
				}
			break;
			case 7 : //white screen
				float visibility2 = t/timer;
				canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, visibility2);
				t -= Time.deltaTime;
				if(t<=0){
					happened = true;
				}
			break;
		}

		// Texte apparait en fondu
		// YVAN LET'S TALK ABOUT THIS LATER
		// BUT WHAT HAPPENS NOW???
		if(happened && fade && colorAlpha<254){
			colorAlpha += 2;
			textEndGame.color = new Color32(0, 0, 0, colorAlpha);
			textEndGame.gameObject.SetActive(true);
		}else{
			fade = false;
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Kid"))
		{
			print("go ending");
			start = true;
		}
	}
	
	
	void stepUp(){
		step += 1;
		done = false;
	}
}
