using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePlaceScript : MonoBehaviour
{
	public bool hasWood = true;
	public bool isLit = true;
	
	
	float healKidTimer;
	float healLightTimer;
	
	[SerializeField] float healTimer = 0.1f;
	
	[SerializeField] float lifeToGive;
	
	public int sparkleIteration = 0;
	float sparkleTimer;
	[SerializeField] public int nbSparkleToLight = 3;
	[SerializeField] float sparkleTimerMax = 0f;
	
	
	bool isKidInside;
	bool isLightInside;
	bool isWoodInside;
	Collider2D kid;
	Collider2D light;
	Collider2D wood;

	Animator animator;
	
    // Start is called before the first frame update
    void Start()
    {
		animator = this.GetComponent<Animator>();
		
		sparkleTimer = sparkleTimerMax;
		
		healKidTimer = healTimer;
		healLightTimer = healTimer;
		
		if( lifeToGive == null || lifeToGive == 0){lifeToGive = 5f;}
		
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);
    }

    // Update is called once per frame
    void Update()
    {
		if(isLit && hasWood){
			animator.SetInteger ("state", 2);
			sparkleIteration = nbSparkleToLight;
			if(isKidInside){
				healKidTimer -= Time.deltaTime;
				if(healKidTimer <= 0 ){
					healKidTimer = healTimer;
					kid.GetComponent<kidScript>().SetLife(lifeToGive);
				}
			}else{
				healKidTimer = healTimer;
			}
			if(isLightInside){
				healLightTimer -= Time.deltaTime;
				if(healLightTimer <= 0 ){
					healLightTimer = healTimer;
					light.GetComponent<lightScript>().SetLight(lifeToGive);
				}
			}else{
				healLightTimer = healTimer;
			}
		}else if(hasWood){
			animator.SetInteger ("state", 1);
			if(isLightInside && light.GetComponent<lightScript>().isCharging){
				sparkleTimer -= Time.deltaTime;
				if(sparkleTimer <= 0 && sparkleIteration != nbSparkleToLight){
					sparkleIteration++;
					sparkleTimer = sparkleTimerMax;
					print("Ting!");
				}
				if(sparkleIteration == nbSparkleToLight){
					LightUp();
					gameObject.GetComponent<SonFeuScript>().playLightUpSound();
				}
			}else{
				sparkleTimer = sparkleTimerMax;
				sparkleIteration = 0;
			}
		}
		
		if(isWoodInside){
			if(!wood.gameObject.GetComponent<itemScript>().isCarried && !hasWood){
				hasWood = true;
				animator.SetInteger ("state", 1);
				Destroy(wood.gameObject);
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Kid"){
			isKidInside = true;
			kid = other;
		}
		if(other.gameObject.tag == "Player"){
			isLightInside = true;
			light = other;
			if(isLit){
				other.gameObject.GetComponent<lightScript>().SetSpawnPosition(transform.position+new Vector3(0,1,1));
			}
		}
		if(IsItWood(other)){
			isWoodInside = true;
			wood = other;
		}
	}
	
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Kid"){
			isKidInside = false;
			healKidTimer = 1f;
		}
		if(other.gameObject.tag == "Player"){
			isLightInside = false;
			healLightTimer = 1f;
		}
		if(IsItWood(other)){
			isWoodInside = false;
		}
	}
	
	bool IsItWood(Collider2D other){
		if(other.gameObject.tag == "Item"){
			if(other.gameObject.GetComponent<itemScript>().type == "wood"){	
				return true;
			}else{
				return false;
			}
		}else{
			return false;
		}
	}
	
	public void LightUp(){
		isLit = true;
		animator.SetInteger ("state", 2);
	}
}
