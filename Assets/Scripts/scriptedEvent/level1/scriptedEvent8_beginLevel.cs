using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scriptedEvent8_beginLevel : MonoBehaviour
{
	
	[SerializeField] float timer = 1;
	float t;
	
    // Start is called before the first frame update
    void Start()
    {
        t = timer;
    }

    // Update is called once per frame
    void Update()
    {
        float visibility = (t/timer);
		transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, visibility);
		t -= Time.deltaTime;
		if(t<=0){
			Destroy(GetComponent<scriptedEvent8_beginLevel>());
		}
    }
}
