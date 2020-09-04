using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptedEvent3_cascade_V2 : MonoBehaviour
{
	
	[SerializeField] GameObject iceToMelt;
	[SerializeField] GameObject spriteRiviere;
	[SerializeField] GameObject tronc;
	[SerializeField] GameObject invisibleWallToErase;
	
	GameObject target;
	
	float t;
	[SerializeField] float timer = 3;
	
	bool start = false;
	bool done = false;
	
	void Start(){
		//Riviere starts invisible
		spriteRiviere.GetComponent<Image>().color = new Color(255,255,255, 0);
		tronc.GetComponent<Image>().color = new Color(255,255,255, 0);

		t = timer;
	}

    // Update is called once per frame
    void Update()
    {
		if(iceToMelt == null && !start){
			activate();
		}
		
        if(!start || done){
			return;
		}
		
		
		
		if(!done){
			t -= Time.deltaTime;
			float visibility = (t/timer);
			spriteRiviere.GetComponent<Image>().color = new Color(255,255,255, visibility);
		}
		if(t <= 0){
			Destroy(invisibleWallToErase);
			tronc.GetComponent<Image>().color = new Color(255, 255, 255, 255);
			done = true;
		}
    }
	
	public void activate(){
		start = true;
	}
}
