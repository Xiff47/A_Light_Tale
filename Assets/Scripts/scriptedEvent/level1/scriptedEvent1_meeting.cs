using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptedEvent1_meeting : MonoBehaviour
{
	
	[SerializeField] GameObject mainCamera;
	[SerializeField] GameObject target;
	[SerializeField] GameObject light;
	[SerializeField] public float timer = 3;
	
	bool start = false;
	bool happened = false;
	
	float t;
	
	int step = 0;
	bool done = false;
	
	float targetDistance;
	
    // Start is called before the first frame update
    void Start()
    {
		t = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!start || happened){
			return;
		}
		/*
		* Step 0 : light stops, camera goes to target
		* Step 1 : camera waits 3 sec
		* Step 2 : camera goes back to light
		* Step 3 : light moves
		*/
		
		switch(step){
			case 0 : 
				if(!done){
					light.GetComponent<lightScript>().toggleActive();
					mainCamera.GetComponent<cameraScript>().followTarget(target.transform.position);
					done = true;
				}
				targetDistance = Mathf.Sqrt(Mathf.Pow((mainCamera.transform.position.x - target.transform.position.x),2)+Mathf.Pow((mainCamera.transform.position.y - target.transform.position.y),2));
				if(targetDistance <= 0.8){
					stepUp();
				}
			break;
			case 1 :
				t -= Time.deltaTime;
				if(t <= 0){
					stepUp();
				}
			break;
			case 2 :
				if(!done){
					mainCamera.GetComponent<cameraScript>().setFollowLight();
					done = true;
				}
				targetDistance = Mathf.Sqrt(Mathf.Pow((mainCamera.transform.position.x - light.transform.position.x),2)+Mathf.Pow((mainCamera.transform.position.y - light.transform.position.y),2));
				if(targetDistance <= 1){
					stepUp();
				}
			break;
			case 3 :
				light.GetComponent<lightScript>().toggleActive();
				happened = true;
			break;
			
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
