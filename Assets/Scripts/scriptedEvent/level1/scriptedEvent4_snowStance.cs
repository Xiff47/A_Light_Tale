using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptedEvent4_snowStance : MonoBehaviour
{
    [SerializeField] GameObject camera;
	ParticleSystem ps;
	
	[SerializeField] bool noSnow;
	[SerializeField] bool normalSnow;
	[SerializeField] bool heavySnow;
	
	[SerializeField] float normalSnowSpeed = 0.8f;
	[SerializeField] float normalSnowParticleNb = 200f;
	
	[SerializeField] float heavySnowSpeed = 2f;
	[SerializeField] float heavySnowParticleNb = 400f;

	
	float newSnowSpeed = 0f;
	float newSowParticleNb = 0f;
	
	void Start(){
		ps = camera.transform.GetChild(0).GetComponent<ParticleSystem>();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			ChangeSnowStance();
		}
	}
	
	public void SetHeavySnow(){
		noSnow = false;
		normalSnow = false;
		heavySnow = true;
		ChangeSnowStance();
	}
	
	public void SetNormalSnow(){
		noSnow = false;
		normalSnow = true;
		heavySnow = false;
		ChangeSnowStance();
	}
	
	public void SetNoSnow(){
		noSnow = true;
		normalSnow = false;
		heavySnow = false;
		ChangeSnowStance();
	}
	
	void ChangeSnowStance(){
		if(!noSnow){
			if(normalSnow){
				newSnowSpeed = normalSnowSpeed;
				newSowParticleNb = normalSnowParticleNb;
			}else if(heavySnow){
				newSnowSpeed = heavySnowSpeed;
				newSowParticleNb = heavySnowParticleNb;
			}
		}else{
			newSnowSpeed = 0f;
			newSowParticleNb = 0f;
		}
		
		var main = ps.main;
		main.simulationSpeed = newSnowSpeed;
		main.maxParticles = Mathf.RoundToInt(newSowParticleNb);
	}
}
