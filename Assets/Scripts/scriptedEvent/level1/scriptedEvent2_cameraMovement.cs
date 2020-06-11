using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptedEvent2_cameraMovement : MonoBehaviour
{
	[SerializeField] Camera mainCamera;
	
	[SerializeField] float closeUpValue = 5f;
	[SerializeField] float mediumValue = 11f;
	[SerializeField] float farValue = 17f;
	
	[SerializeField] bool closeUp = false;
	[SerializeField] bool medium = true;
	[SerializeField] bool far = false;
	
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			setting();
		}
	}
	
	public void SetFar(){
		closeUp = false;
		medium = false;
		far = true;
		setting();
	}
	
	public void SetNormal(){
		closeUp = false;
		medium = true;
		far = false;
		setting();
	}
	
	public void SetClose(){
		closeUp = true;
		medium = false;
		far = false;
		setting();
	}
	
	void setting(){
		if(closeUp){
			mainCamera.GetComponent<cameraScript>().SetZoom(closeUpValue);
		}else if(far){
			mainCamera.GetComponent<cameraScript>().SetZoom(farValue);
		}else{
			mainCamera.GetComponent<cameraScript>().SetZoom(mediumValue);
		}
	}
}
