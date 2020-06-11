using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptedEvent3_cascade : MonoBehaviour
{
	
	[SerializeField] GameObject iceToMelt;
	[SerializeField] GameObject riviere;
	[SerializeField] GameObject invisibleWallToErase;
	
	GameObject target;
	
	float t = 0.5f;
	
	bool start = false;
	bool done = false;
	
	float targetDistance;
	
	int step = 0;
	int maxStep;
	
	void Start(){
		maxStep = riviere.transform.childCount;
		print(maxStep);
	}

    // Update is called once per frame
    void Update()
    {
		if(iceToMelt == null && !start){
			activate();
		}
		
        if(!start){
			return;
		}
		
		
		
		if(!done){
			t -= Time.deltaTime;
		}
		if(t <= 0){
			
			riviere.transform.GetChild(step).gameObject.SetActive(true);
			
			if(step == maxStep-2){
				Destroy(invisibleWallToErase);
			}
			if(step == maxStep-1){
				done = true;
				gameObject.GetComponent<scriptedEvent2_cameraMovement>().SetNormal();
			}
			step++;
			t = 0.5f;
		}
    }
	
	public void activate(){
		start = true;
		gameObject.GetComponent<scriptedEvent1_meeting>().activate();
		gameObject.GetComponent<scriptedEvent2_cameraMovement>().SetFar();
	}
}
