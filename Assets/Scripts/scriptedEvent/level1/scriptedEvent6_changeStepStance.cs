using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptedEvent6_changeStepStance : MonoBehaviour
{
	[SerializeField] bool isGoingIndoor = false;
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Kid"){
            print("il sort");
            other.gameObject.GetComponent<kidScript>().isIndoor = isGoingIndoor;
		}
	}
}
