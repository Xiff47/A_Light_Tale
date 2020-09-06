using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidSpecialFollow : MonoBehaviour
{
	/*
	float lightDistance;
	Vector2 lightDirection;
	Vector2 lightPosition;
	
	float currentSpeed;
	float speedMultiplifier;
	
	bool go = false;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if(!go){ return; }
		
		currentSpeed = gameObject.GetComponent<kidScript>().currentSpeed;
		speedMultiplifier = gameObject.GetComponent<kidScript>().speedMultiplifier;
		
		lightDistance = Mathf.Sqrt(Mathf.Pow((lightPosition.x - transform.position.x),2)+Mathf.Pow((lightPosition.y - transform.position.y),2));
		lightDirection = (lightPosition - transform.position).normalized;

		if(lightDistance <= gameObject.GetComponent<kidScript>().FARDISTANCE){
			if(lightDistance <= gameObject.GetComponent<kidScript>().MEDIUMDISTANCE){
				if(lightDistance <= gameObject.GetComponent<kidScript>().CLOSEDISTANCE){
					//lumière proche
					gameObject.GetComponent<kidScript>().currentSpeed = gameObject.GetComponent<kidScript>().WALKINGSPEED;
					if(lightDistance >= gameObject.GetComponent<kidScript>().closeDistanceStop){
						gameObject.GetComponent<kidScript>().isWalking = true;
						gameObject.GetComponent<kidScript>().isJogging = false;
						gameObject.GetComponent<kidScript>().isRunning = false;
						gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("vitesse", 1);
					}
				}else if(isFollowing){
					//lumière dist moyenne
					gameObject.GetComponent<kidScript>().currentSpeed = gameObject.GetComponent<kidScript>().JOGGINGSPEED;
					gameObject.GetComponent<kidScript>().isWalking = false;
					gameObject.GetComponent<kidScript>().isJogging = true;
					gameObject.GetComponent<kidScript>().isRunning = false;
					gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("vitesse", 2);
				}
			}else if(isFollowing && !isCarryingItem){
				//lumière éloignée 
				gameObject.GetComponent<kidScript>().currentSpeed = gameObject.GetComponent<kidScript>().RUNNINGSPEED;
				gameObject.GetComponent<kidScript>().isWalking = false;
				gameObject.GetComponent<kidScript>().isJogging = false;
				gameObject.GetComponent<kidScript>().isRunning = true;
				gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("vitesse", 3);
			}
			
			// Gestion de la direction de l'enfant par rapport à la lumière
			if(Mathf.Abs(lightDirection.x) > Mathf.Abs(lightDirection.y))
			{
				//Horizontal
				if(lightDirection.x > 0){
					gameObject.GetComponent<kidScript>().kidDirection = EST;
					gameObject.GetComponent<kidScript>().kidHorizontalDirection = 1;
					transform.localScale = new Vector3(kidSize, kidSize, kidSize);
					gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("direction", EST);
					
				}
				else {
					gameObject.GetComponent<kidScript>().kidDirection = WEST;
					gameObject.GetComponent<kidScript>().kidHorizontalDirection = -1;
					transform.localScale = new Vector3(-kidSize, kidSize, kidSize);
					gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("direction", EST); // EST pour avoir la valeur qui correspond juste à l'animation de profil mais en inversant l'image					
				}
			}
			else 
			{
				//Vertical
				if(lightDirection.y > 0)
				{
					gameObject.GetComponent<kidScript>().kidDirection = NORTH;
				}
				else
				{
					gameObject.GetComponent<kidScript>().kidDirection = SOUTH;
				}
				if(kidDirection == NORTH){
					gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("direction", NORTH);
				}else{
					gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("direction", SOUTH);
				}
			}
		}
		if(gameObject.GetComponent<kidScript>().isPickingUp == false){
			if(lightDistance >= gameObject.GetComponent<kidScript>().closeDistanceStop){
				transform.position = Vector3.MoveTowards(transform.position, lightPosition, currentSpeed*speedMultiplifier*Time.deltaTime);
			}else{
				gameObject.GetComponent<kidScript>().isWalking = false;
				gameObject.GetComponent<kidScript>().isJogging = false;
				gameObject.GetComponent<kidScript>().isRunning = false;
				gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("vitesse", 0);
				//Kid arrete de bouger s'il est trop proche
			}
		}else{
			if(Mathf.Sqrt(Mathf.Pow((itemClose.transform.position.x - transform.position.x),2)+Mathf.Pow((itemClose.transform.position.y - transform.position.y),2)) >= 0.1f){
				transform.position = Vector3.MoveTowards(transform.position, gameObject.GetComponent<kidScript>().itemClose.transform.position, currentSpeed * speedMultiplifier * Time.deltaTime);
			}else{
				gameObject.GetComponent<kidScript>().PickUp();
			}
		}
		
		
		if(isCarryingItem){
			gameObject.GetComponent<kidScript>().carriedItem.transform.position = new Vector3(transform.position.x + 0.4f * -gameObject.GetComponent<kidScript>().kidHorizontalDirection, transform.position.y + 0.3f, transform.position.z - 0.1f);
			gameObject.GetComponent<kidScript>().kidAnimator.SetBool("isCarryingItem", true);
			if(!isFollowing){
				gameObject.GetComponent<kidScript>().kidAnimator.SetFloat("vitesse", 0);
			}
			
			gameObject.GetComponent<kidScript>().carriedItem.gameObject.SetActive(false);
		}else{
			gameObject.GetComponent<kidScript>().kidAnimator.SetBool("isCarryingItem", false);
			if(carriedItem != null){
				gameObject.GetComponent<kidScript>().carriedItem.gameObject.SetActive(true);
			}
		}
    }
	
	public void activate(Vector2 specialTarget){
		lightPosition = specialTarget;
		go = start;
	}
	*/
}
