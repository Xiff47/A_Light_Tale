using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightScript : MonoBehaviour
{
	
	bool lightStart;
	bool active = true;
	bool followSpecialTarget = false;
	Vector2 specialTarget;

	float light = 100f;
	public Vector3 spawnPosition;
	[SerializeField] GameObject spawnDebutDeNiveau;
	public bool isRespawning = false;
	
	[SerializeField] public GameObject kid;
	Vector3 kidPosition;
	float kidDistance;
	float mouseDistance;
	Vector3 mousePosition;
	bool childIsDed = false;
	
	[SerializeField] private UIBar lightBar;
	[SerializeField] private Image givre;
	public bool acceptGivre = false;
	
	float currentSpeed;
	[SerializeField] float NORMALSPEED;
	[SerializeField] float SLOWSPEED;
	
	bool clicGauche;
	float clicDuration = 0f;
	
	float TimerLoseLight = 1f;
	
	public bool isCharging;
	
	[SerializeField] float distanceCloseStop = 1f;
	[SerializeField] float distanceMaxMoving = 3f;
	[SerializeField] float KIDDISTANCEMAX = 10f;
	
	[SerializeField] float lifeToGiveToKid = 10f;
	[SerializeField] float lightToLooseToKid = 10f;

	Animator lightAnimator;
	Animator gelAnimator;
	Animator kidAnimator; // Animator de l'enfant

	
	[SerializeField] float timerNextGivreStep = 3f;
	float timerNextGivreStepMax;
	int actualGivreStep = 0;
	int givreStepMax = 5;
	
    // Start is called before the first frame update
    void Start()
    {
		lightAnimator = this.GetComponent<Animator>();
		gelAnimator = GameObject.Find("Givre").GetComponent<Animator>();
		kidAnimator = kid.GetComponent<kidScript>().GetComponent<Animator>();

		spawnPosition = spawnDebutDeNiveau.transform.position;
		transform.position = spawnPosition;
		currentSpeed=NORMALSPEED;
		
		if(NORMALSPEED == null || NORMALSPEED == 0) NORMALSPEED = 3f;
		if(SLOWSPEED == null || SLOWSPEED == 0) SLOWSPEED = 1f;
		
		timerNextGivreStepMax = timerNextGivreStep;
	}

    // Update is called once per frame
    void Update()
    {
		
		if(!active){
			return;
		}
		
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);


		//distance entre light et mouse;
		if (!followSpecialTarget)
		{
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else
		{
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		mousePosition = new Vector3(mousePosition.x,mousePosition.y, transform.position.z);
		mouseDistance = Mathf.Sqrt(Mathf.Pow((mousePosition.x - transform.position.x),2)+Mathf.Pow((mousePosition.y - transform.position.y),2));
		if(mouseDistance >= distanceMaxMoving){
			mouseDistance = distanceMaxMoving;
		}
		
		//distance entre light et kid
		kidPosition = kid.transform.position;
		kidDistance = Mathf.Sqrt(Mathf.Pow((kidPosition.x - transform.position.x),2)+Mathf.Pow((kidPosition.y - transform.position.y),2));
		
		if(!acceptGivre && kid.GetComponent<kidScript>().isFollowing){
			acceptGivre = true;
		}
		if(isRespawning && kid.GetComponent<kidScript>().isFollowing){
			isRespawning = false;
		}
		
		
		//déplacer la lumière
		if(mouseDistance>=distanceCloseStop && lightStart){
			transform.position = Vector3.MoveTowards(transform.position, mousePosition, currentSpeed * mouseDistance * Time.deltaTime);
		}else if(mouseDistance<distanceCloseStop){
			lightStart = true;
		}
		
		//clic souris
		if(Input.GetMouseButtonDown(0)){
			clicGauche = true;
		}
		if(clicGauche){
			clicDuration += Time.deltaTime;
			if(clicDuration>=0.5){
				currentSpeed = SLOWSPEED;
				if(light > 0){
					isCharging = true;
					lightAnimator.SetBool("clicGauche", true);
				}
				else{
					isCharging = false;
					lightAnimator.SetBool("clicGauche", false);
				}
				TimerLoseLight -= Time.deltaTime;
				if(TimerLoseLight <= 0){
					TimerLoseLight = 1f;
					if(lightToLooseToKid >= 0){ lightToLooseToKid = -lightToLooseToKid;}
					SetLight(lightToLooseToKid);
					if(kidDistance <= 3f){
						kid.GetComponent<kidScript>().SetLife(lifeToGiveToKid);
						if(kid.GetComponent<kidScript>().hp < 100){
							print("hp < 100 --> Enfant SeRechauffe = true");
							kidAnimator.SetBool("SeRechauffe", true);
						}else{
							print("hp == 100 --> Enfant NeSeRechauffePas");
							kidAnimator.SetBool("SeRechauffe", false);
						}									
					}
					if(kid.GetComponent<kidScript>().isDed){
						kid.GetComponent<kidScript>().isDed = false;
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0)){
			if(clicDuration <= 0.5f){
				kid.gameObject.GetComponent<kidScript>().CheckItem();
			}
			clicGauche = false;
			lightAnimator.SetBool("clicGauche", false);
			kidAnimator.SetBool("SeRechauffe", false);
			clicDuration = 0;
			currentSpeed = NORMALSPEED;
			isCharging = false;
		}
		
		transform.GetChild(1).GetComponent<boussoleScript>().SetVisibility(kidDistance>KIDDISTANCEMAX);
		setGivreTimer(((kidDistance > KIDDISTANCEMAX) || childIsDed) && acceptGivre && !isRespawning);
		//setGivreTimer(((kidDistance > KIDDISTANCEMAX) || childIsDed) && acceptGivre);
		
		/*print("acceptGivre :"+acceptGivre);
		print("isRespawning :"+isRespawning);
		print("(kidDistance > KIDDISTANCEMAX) :"+(kidDistance > KIDDISTANCEMAX));
		print("childIsDed :"+childIsDed);
		print(((kidDistance > KIDDISTANCEMAX) || childIsDed) && acceptGivre && !isRespawning);*/
		
		/* OLD GIVRE WORKING
		if(kidDistance > KIDDISTANCEMAX && !isRespawning && acceptGivre){
			//NEED TO CHECK WITH GD
			// HOW DOES GIVRE WORKS?????
			timerBeforeGivre -= Time.deltaTime;
			timerDeath -= Time.deltaTime * 0.5f;
			if(timerDeath <= 0){
				Die();
			}
		}else if(timerDeath < 10){
			timerDeath += 3 * Time.deltaTime;
		}else{
			timerDeath = 10f;
		}
		newGivreColor = (byte) (255 - timerDeath * 25);
		givre.color = new Color32(0,0,0, newGivreColor);*/
		
		// if(light >= 75){
		// 	lightAnimator.SetFloat("vieLueur", 75);
		// }else if (light >= 50){
		// 	lightAnimator.SetFloat("vieLueur", 50);
		// }
		// else if (light >= 25){
		// 	lightAnimator.SetFloat("vieLueur", 25);
		// }

		lightAnimator.SetFloat("vieLueur", light);
		transform.localScale = new Vector3((float)(0.5+(light/200)), (float)(0.5+(light/200)), 1f);

		if(light <= 0){
			if(childIsDed){
				Die();
			}
		}
    }
	
	// EN POURCENTAGE!!!!
	public void SetLight(float lightPourcentage){
		light += lightPourcentage;
		if(light > 100){
			light = 100;
			lightBar.SetSize(1);
		}else if(light > 0){
			lightBar.SetSize(light/100);
		}else{
			light = 0;
			lightBar.SetSize(0);
		}
	}
	
	public void SetSpawnPosition(Vector3 newSpawnPosition){
		spawnPosition = newSpawnPosition;
	}
	
	void Die(){
		transform.position = spawnPosition;
		SetLight(100);
		lightStart = false;
		clicGauche = false;
		clicDuration = 0;
		currentSpeed = NORMALSPEED;
		isCharging = false;
		isRespawning = true;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == LayerMask.NameToLayer("LightNoCollision") || other.gameObject.layer == 9 || other.gameObject.tag == "Kid"){
			Physics2D.IgnoreCollision(other.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>(), true); 
		}
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.layer == LayerMask.NameToLayer("LightNoCollision") || other.gameObject.layer == 9 || other.gameObject.tag == "Kid"){
			Physics2D.IgnoreCollision(other.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>(), true); 
		}
	}
	
	public void toggleActive(){
		active = !active;
	}
	
	public void SetChildStance(bool newChildStance){
		childIsDed = newChildStance;
	}
	
	public void setGivreTimer(bool startingGivre){
		if(startingGivre){
			timerNextGivreStep -= Time.deltaTime;
			if(timerNextGivreStep <= 0){
				timerNextGivreStep = timerNextGivreStepMax + actualGivreStep * 1.3f ;
				actualGivreStep++;
				print("Givre state : "+actualGivreStep);
				if(actualGivreStep > givreStepMax){
					Die();
					if(actualGivreStep > givreStepMax+1){
						print("ERROR DETECTED");
						actualGivreStep = 0;
					}
					timerNextGivreStep = 0;
				}
			}
		}else{
			if(actualGivreStep != 0){
				if(timerNextGivreStep > 0.2f){
					timerNextGivreStep = 0f;
				}
				//restore vision
				timerNextGivreStep -= Time.deltaTime;
				if(timerNextGivreStep <= 0){
					print("Givre state : "+actualGivreStep);
					timerNextGivreStep = 0.2f;
					actualGivreStep--;
				}
			}
		}
		// Mise à jour de l'affichage du givre
		gelAnimator.SetFloat("state", actualGivreStep);

	}

	public void SpecialFollow(Vector2 target)
	{
		followSpecialTarget = true;
		specialTarget = target;
	}

	public void StopSpecialFollow()
	{
		followSpecialTarget = false;
	}
}
