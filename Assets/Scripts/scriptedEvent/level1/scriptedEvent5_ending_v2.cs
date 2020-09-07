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
	[SerializeField] GameObject creditsScreen;
	
	bool start = false;
	bool happened = false;
	bool fade = false;
	
	float t;
	
	int step = 0;
	bool done = false;
	
	float targetDistance;

	Animator kidAnimator;
	Animator m_Animator;
    string m_ClipName;
    AnimatorClipInfo[] m_CurrentClipInfo;
	
	
	
    // Start is called before the first frame update
    void Start()
    {
		kidAnimator = kid.GetComponent<Animator>();

		t = timer;
		textEndGame.color = new Color(100f/255f,91f/255f,246f/255f, 0.0f);
		creditsScreen.GetComponent<Image>().color = new Color(1,1,1, 0);
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
		
		//Get them_Animator, which you attach to the GameObject you intend to animate.
        m_Animator = kidAnimator;
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        //Access the Animation clip name
        m_ClipName = m_CurrentClipInfo[0].clip.name;
		
		print(m_ClipName);
		
		switch (step){
			case 0 : // kid goes to fireplace
				if(!done){
					kid.GetComponent<kidScript>().SpecialFollow(kidPositionNearFire.transform.position);
					light.GetComponent<lightScript>().SpecialFollow(lightDestination.transform.position);
					light.GetComponent<lightScript>().toggleActive();
					light.GetComponent<lightScript>().acceptGivre = false;
					done = true;
				}
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - kidPositionNearFire.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - kidPositionNearFire.transform.position.y),2)) <= kid.GetComponent<kidScript>().closeDistanceStop){
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
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - wood.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - wood.transform.position.y),2)) <= kid.GetComponent<kidScript>().closeDistanceStop){
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
				if(Mathf.Sqrt(Mathf.Pow((kid.transform.position.x - kidPositionNearFire.transform.position.x),2)+Mathf.Pow((kid.transform.position.y - kidPositionNearFire.transform.position.y),2)) <= kid.GetComponent<kidScript>().closeDistanceStop){
					kid.GetComponent<kidScript>().CheckItem();
					stepUp();
				}
			break;
			case 5 : //kid lights fire
				if(!done){
					// YVAN DO YOUR MAGIC
					kidAnimator.SetTrigger("AllumeFeu"); // Animatin de l'enfant qui allume le feu
					done = true;
				}
				if(m_ClipName == "EnfantIdleDosAnim"){ // when anim finished YVAN DO YOUR MAGIC
					fire.GetComponent<firePlaceScript>().LightUp();
					print("fire lit up");
					stepUp();
					t = 3;
				}
			break;
			case 6 :
				t -= Time.deltaTime;
				if(t<=0){
					timer = 5;
					t = timer;
					stepUp();
				}
			break;
			case 7 : //white screen + end game text
				float visibility2 = t/timer;
				canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, 1-visibility2);
				textEndGame.color = new Color(100f/255f,91f/255f,246f/255f, 1-visibility2);
				t -= Time.deltaTime;	
				if(t<=0){
					//happened = true;
					timer = 3;
					t = timer;
					stepUp();
				}
			break;
			case 8 : // end game text disappear
				float visibility3 = t/timer;
				print("visibility3 = " + visibility3);
				textEndGame.color = new Color(100f/255f,91f/255f,246f/255f, visibility3);
				t -= Time.deltaTime;
				if(t<=0){
					timer = 4;
					t = timer;
					stepUp();
				}
			break;
			case 9 : // credits panel 
				float visibility4 = t/timer;

				creditsScreen.GetComponent<Image>().color = new Color(1,1,1, 1-visibility4);
				t -= Time.deltaTime;	
				if(t<=0){
					happened = true;
				}
			break;
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
