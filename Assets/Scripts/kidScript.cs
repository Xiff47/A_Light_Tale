using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidScript : MonoBehaviour
{
	//hp en pourcentage
	public float hp = 100f;
	public bool isDed;
	bool hasStartedplaying = false;
 
	
	[SerializeField] new GameObject light;
	Vector3 lightPosition;
	float lightDistance;
	Vector2 lightDirection;
	
	public bool followSpecialTarget = false;
	Vector2 specialTarget;
	
	[SerializeField] public bool canStep = false;
	[SerializeField] GameObject footStep;
	Vector2 offsetFootstepSetup = new Vector2(0.3f,0.1f);
	Vector2 offsetFootstep;
	bool droite = true;
	
	public bool isFollowing = false;
	bool startToGetCold = false;
	
	bool isWalking = false;
	bool isJogging = false;
	bool isRunning = false;
	bool isFalling = false;
	Vector3 fallBackPosition;
	float fallBackDistance;
	string fallBackReason = "";
    [SerializeField] public bool isIndoor = true;
	
	const int NORTH = 1;
	const int EST = 2;
	const int SOUTH = 3;
	const int WEST = 4;
	float kidSize; // Prendra la taille en X de la valeur renseignée dans l'éditeur
	int kidDirection;
	int kidHorizontalDirection;
	
	[SerializeField] float closeDistanceStop = 2f;
	[SerializeField] float CLOSEDISTANCE = 4f;
	[SerializeField] float MEDIUMDISTANCE = 8f;
	[SerializeField] float FARDISTANCE = 12f;
	
	[SerializeField] float WALKINGSPEED;
	[SerializeField] float JOGGINGSPEED;
	[SerializeField] float RUNNINGSPEED;
	float speedMultiplifier;
	float currentSpeed;
	
	public bool isLost;
	int fallTest;
	
	[SerializeField] private UIBar healthBar;
	
	public bool isCold;
	float coldTimer = 0.2f;
	
	float coldDamage;
	[SerializeField] float COLDDAMAGEUSUAL = 1;
	[SerializeField] float BLIZZARDDAMAGE = 1;
	[SerializeField] float BLIZZARDCLOSEDAMAGE = 1;
	[SerializeField] float fallBackDamage = 1;
	
	public bool isInBlizzard = false;
	
	float timerFallTest;
	
	bool isItemClose;
	bool isPickingUp;
	GameObject itemClose;
	
	bool isCarryingItem;
	GameObject carriedItem;

	bool isCoveredByTree = false;
	List<GameObject> coveringTrees = new List<GameObject>();

	public float chanceToFallWhileRunningPerSecond = 30;
	public float chanceToFallWhileJoggingPerSecond = 10;

	Animator kidAnimator;
	bool isStanding = false;
	int etatFroidEnfant = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		kidAnimator = this.GetComponent<Animator>();
		kidAnimator.SetFloat("direction", SOUTH);
		kidSize = transform.localScale.x;

		isCold = true;
		isLost = true;
		speedMultiplifier = 1f;
		kidDirection = SOUTH;
		fallBackPosition = new Vector3(0,0,0);
		fallBackDistance = 0f;
		
		if(COLDDAMAGEUSUAL == null || COLDDAMAGEUSUAL == 0) COLDDAMAGEUSUAL = -0.3f;
		if(BLIZZARDDAMAGE == null || BLIZZARDDAMAGE == 0) BLIZZARDDAMAGE = -0.8f;
		if(BLIZZARDCLOSEDAMAGE == null || BLIZZARDCLOSEDAMAGE == 0) BLIZZARDCLOSEDAMAGE = -0.5f;
		
		if(WALKINGSPEED == null || JOGGINGSPEED == 0) WALKINGSPEED = 4f;
		if(JOGGINGSPEED == null || JOGGINGSPEED == 0) JOGGINGSPEED = 6f;
		if(RUNNINGSPEED == null || JOGGINGSPEED == 0) RUNNINGSPEED = 8f;
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);
		
		
		//calcul de la distance et de la direction entre enfant et lumière
		if(!followSpecialTarget){
			lightPosition = light.transform.position;
		}else{
			lightPosition = specialTarget;
		}
		lightDistance = Mathf.Sqrt(Mathf.Pow((lightPosition.x - transform.position.x),2)+Mathf.Pow((lightPosition.y - transform.position.y),2));
		lightDirection = (lightPosition - transform.position).normalized;
		
		if(!isDed  && !isStanding){
			//vérification du multiplicateur de vitesse
			speedMultiplifier = SetSpeedMultiplifier();

			if(lightDistance <= FARDISTANCE && !isFalling){
				if(lightDistance <= MEDIUMDISTANCE){
					if(lightDistance <= CLOSEDISTANCE){
						//lumière proche
						currentSpeed = WALKINGSPEED;
						if(!hasStartedplaying){
							hasStartedplaying = true;
						}
						if(lightDistance >= closeDistanceStop){
							isWalking = true;
							isJogging = false;
							isRunning = false;
							kidAnimator.SetFloat("vitesse", 1);
							if(light.gameObject.GetComponent<lightScript>().isRespawning){
								light.gameObject.GetComponent<lightScript>().isRespawning = false;
							}
						}
						isFollowing = true;
						startToGetCold = true;
						kidAnimator.SetInteger("kidFalling", 0);
						kidAnimator.SetFloat("falling", 0);
						isLost = false;
						isCold = false;
					}else if(isFollowing){
						//lumière dist moyenne
						currentSpeed = JOGGINGSPEED;
						isWalking = false;
						isJogging = true;
						isRunning = false;
						kidAnimator.SetFloat("vitesse", 2);
						isCold = true;
					}
				}else if(isFollowing && !isCarryingItem){
					//lumière éloignée
					currentSpeed = RUNNINGSPEED;
					isWalking = false;
					isJogging = false;
					isRunning = true;
					kidAnimator.SetFloat("vitesse", 3);
					isCold = true;
				}
				
				// Gestion de la direction de l'enfant par rapport à la lumière
				if(Mathf.Abs(lightDirection.x) > Mathf.Abs(lightDirection.y))
				{
					//Horizontal
					if(lightDirection.x > 0){
						if(isFollowing){
							kidDirection = EST;
							kidHorizontalDirection = 1;
							transform.localScale = new Vector3(kidSize, kidSize, kidSize);
							kidAnimator.SetFloat("direction", EST);
						}
					}
					else {
						if(isFollowing){
							kidDirection = WEST;
							kidHorizontalDirection = -1;
							transform.localScale = new Vector3(-kidSize, kidSize, kidSize);
							kidAnimator.SetFloat("direction", EST); // EST pour avoir la valeur qui correspond juste à l'animation de profil mais en inversant l'image
						}						
					}
				}
				else 
				{
					//Vertical
					if(lightDirection.y > 0)
					{
						kidDirection = NORTH;
					}
					else
					{
						kidDirection = SOUTH;
					}
				
					if(isFollowing){
						if(kidDirection == NORTH)
							kidAnimator.SetFloat("direction", NORTH);
						else
							kidAnimator.SetFloat("direction", SOUTH);
					}
				}
				
				
			}else if(isFollowing){
				//l'enfant est très loin
				GetLost();
				timerFallTest = 1f;
				isFollowing = false;
				kidAnimator.SetFloat("falling", 1);
				kidDirection = SOUTH;
			}
			
			if(isFollowing && !isPickingUp && !isFalling){
				//déplacement de l'enfant s'il suit la lumière
				if(lightDistance>=closeDistanceStop){
					transform.position = Vector3.MoveTowards(transform.position, lightPosition, currentSpeed*speedMultiplifier*Time.deltaTime);
				}else{
					isWalking = false;
					isJogging = false;
					isRunning = false;
					kidAnimator.SetFloat("vitesse", 0);
					//Kid arrete de bouger s'il est trop proche
				}
				if(isJogging || isRunning){
					// gestion de la chute de l'enfant
					timerFallTest -= Time.deltaTime;
					if(timerFallTest <= 0){
						FallTest();
					}
				}
			}else if(isFalling){
				if(fallBackDistance >= 0.6f){
					fallBackDistance = Mathf.Sqrt(Mathf.Pow((fallBackPosition.x - transform.position.x),2)+Mathf.Pow((fallBackPosition.y - transform.position.y),2));
					transform.position = Vector3.MoveTowards(transform.position, fallBackPosition, currentSpeed * speedMultiplifier * fallBackDistance * Time.deltaTime);
				}else{
					fallBackDistance = 0;
					fallBackPosition = new Vector3 (0,0,0);
					FallDown(fallBackReason);
				}
			}else if(isPickingUp){
				if(Mathf.Sqrt(Mathf.Pow((itemClose.transform.position.x - transform.position.x),2)+Mathf.Pow((itemClose.transform.position.y - transform.position.y),2)) >= 0.1f){
					transform.position = Vector3.MoveTowards(transform.position, itemClose.transform.position, currentSpeed*speedMultiplifier * Time.deltaTime);
				}else{
					PickUp();
				}
			}
			
			//gestion du dommage de froid
			if(isInBlizzard && !isCoveredByTree){
				if(isWalking){
					coldDamage = BLIZZARDCLOSEDAMAGE;
				}else{
					coldDamage = BLIZZARDDAMAGE;
				}
			}else{
				coldDamage = COLDDAMAGEUSUAL;
			}
			
			if(isCold || isInBlizzard && startToGetCold){
				coldTimer -= Time.deltaTime;
				if(coldTimer <= 0){
					coldTimer = 0.2f;
					if(coldDamage >= 0){coldDamage=-coldDamage;}
					SetLife(coldDamage);
				}
			}else{
				coldTimer = 0.2f;
			}
			
			if(hp <= 0){
				Die();
			}
			
			if(isCarryingItem){
				carriedItem.transform.position = new Vector3(transform.position.x + 0.4f * -kidHorizontalDirection, transform.position.y + 0.3f, transform.position.z - 0.1f);
				kidAnimator.SetBool("isCarryingItem", true);
				if(!isFollowing){
					kidAnimator.SetFloat("vitesse", 0);
				}
				
				carriedItem.gameObject.SetActive(false);
			}else{
				kidAnimator.SetBool("isCarryingItem", false);
				if(carriedItem != null){
					carriedItem.gameObject.SetActive(true);
				}
				
			}

			if(hp > 60){
				kidAnimator.SetInteger("Froid", 0);
			}else if (hp > 30 && hp <= 60){
				kidAnimator.SetInteger("Froid", 1);
			}else{
				kidAnimator.SetInteger("Froid", 2);
			}
		}
    }

	void StepRightFeet()
	{
		if (!canStep)
		{
			return;
		}
		offsetFootstep = new Vector2(offsetFootstepSetup.x * lightDirection.y, offsetFootstepSetup.y * lightDirection.x);
		GameObject newFootstep = (GameObject)Instantiate(footStep, new Vector3(transform.position.x + offsetFootstep.x, transform.position.y + offsetFootstep.y, transform.position.z + 0.1f), Quaternion.Euler(new Vector3(lightDirection.x, lightDirection.y, 0)));
		gameObject.GetComponent<SonFootstepScript>().PlayStepSound();
	}
	void StepLeftFeet(){
		if(!canStep){
			return;
		}
		offsetFootstep = new Vector2(offsetFootstepSetup.x * lightDirection.y, offsetFootstepSetup.y * lightDirection.x);
		offsetFootstep *= -1; // Inverser l'image pour le pied gauche
		GameObject newFootstep = (GameObject)Instantiate(footStep, new Vector3(transform.position.x+offsetFootstep.x, transform.position.y+offsetFootstep.y, transform.position.z+0.1f), Quaternion.Euler(new Vector3(lightDirection.x, lightDirection.y, 0)));
		gameObject.GetComponent<SonFootstepScript>().PlayStepSound();
	}

	void Standing(){
		isStanding = !isStanding;
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Item"){
			isItemClose = true;
			itemClose = other.gameObject;
		}else if (other.gameObject.tag == "Tree")
		{
			if (isRunning)
			{
				Fallback("tree");
			}
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Item"){
			isItemClose = false;
		}
	}
	
	float SetSpeedMultiplifier(){
		if(isFalling){
			return 75f;
		}else if(isInBlizzard){
			return 0.5f;
		}else if(isCarryingItem){
			return 0.7f;
		}else{
			return 1f;
		}
	}

	public void GetLost(){
		if (followSpecialTarget) { return; }
		isLost = true;
		isCold = true;
		isWalking = false;
		isJogging = false;
		isRunning = false;
		isFollowing = false;
	}
	
	public bool isKidLost(){
		return isLost;
	}
	
	public void FallDown(string fallReason = "speed"){
		if (followSpecialTarget)
		{
			return;
		}
		if(fallReason != "speed"){
			if(fallReason == "wolf"){
				print("Je tombes à cause d'un loup !");
                gameObject.GetComponent<SonEnfantScript>().PlayScreamSound();
            }
            else if(fallReason == "tree"){
				print("Je tombes à cause d'un arbre !");
                gameObject.GetComponent<SonEnfantScript>().PlayFallDownTreeSound();
            }
			isFalling = false;
			SetLife(fallBackDamage);
		}else if(fallReason == "death"){
			print("Je tombes car j'ai froid !");
			kidAnimator.SetInteger("kidFalling", 1); // tombe en avant
			kidAnimator.SetFloat("falling", 1);
            gameObject.GetComponent<SonEnfantScript>().PlayFallDownSound();
		}else{
			print("Je tombes car je vais trop vite !");
			kidAnimator.SetInteger("kidFalling", 1); // tombe en avant
			kidAnimator.SetFloat("falling", 1);
            gameObject.GetComponent<SonEnfantScript>().PlayFallDownSound();
        }
		GetLost();
	}
	
	public void Fallback(string reason){
		kidAnimator.SetInteger("kidFalling", 2); // tombe en arrière
		kidAnimator.SetFloat("falling", 2);
		isFalling = true;
		fallBackReason = reason;
		isFollowing = false;
		currentSpeed = RUNNINGSPEED;
		fallBackPosition = new Vector3(transform.position.x + (2 * lightDirection.x * -1),transform.position.y + (2 * lightDirection.y * -1),0);
		fallBackDistance = Mathf.Sqrt(Mathf.Pow((fallBackPosition.x - transform.position.x),2)+Mathf.Pow((fallBackPosition.y - transform.position.y),2));
	}
    
    public void Fallback(Vector2 wolfPosition, string reason){
		kidAnimator.SetInteger("kidFalling", 2); // tombe en arrière
		kidAnimator.SetFloat("falling", 2);
		isFalling = true;
		fallBackReason = reason;
		isFollowing = false;
		currentSpeed = RUNNINGSPEED;
		Vector2 wolfDirection = (wolfPosition-new Vector2(transform.position.x, transform.position.y)).normalized;;
		fallBackPosition = new Vector3(transform.position.x + (2 * wolfDirection.x * -1),transform.position.y + (2 * wolfDirection.y * -1),0);
		fallBackDistance = Mathf.Sqrt(Mathf.Pow((fallBackPosition.x - transform.position.x),2)+Mathf.Pow((fallBackPosition.y - transform.position.y),2));
	}
	
	void FallTest(){
		timerFallTest = 1f;
		if(isJogging){
			fallTest = Random.Range(1, 31);
			if(fallTest == 1){
				FallDown();
			}
		}else if(isRunning){
			fallTest = Random.Range(1, 11);
			if(fallTest == 1){
				FallDown();
			}
		}
	}
	
	// EN POURCENTAGE!!!!
	public void SetLife(float hpPercentage){
		if(!hasStartedplaying || followSpecialTarget){
			return;
		}
		if(isDed && hpPercentage > 0){
			isDed = false;
			kidAnimator.SetTrigger("stand");
			light.GetComponent<lightScript>().SetChildStance(isDed);
		}
        if (hpPercentage > 0)
        {
            gameObject.GetComponent<SonEnfantScript>().PlayHealSound();
        }
        hp += hpPercentage;
		//kidAnimator.SetBool("SeRechauffe", true);
		if(hp>100){
			healthBar.SetSize(1);
			hp = 100;
		}else if(hp > 0){
			healthBar.SetSize(hp/100);
		}else{
			hp = 0;
			healthBar.SetSize(0);
			Die();
		}
	}

	public int GetCurrentSpeed()
	{
		if (isRunning)
		{
			return 3;
		}
		if (isJogging)
		{
			return 2;
		}
		if (isWalking)
		{
			return 1;
		}
		return 0;
	}
	
	public void BlizzardSet(bool b){
		isInBlizzard = b;
	}
	
	public void CheckItem(){
		if(isCarryingItem){
			isCarryingItem = false;
			carriedItem.transform.position = transform.position;
			carriedItem.gameObject.GetComponent<itemScript>().isCarried = false;
		}else if(isItemClose && !isDed){
			isFollowing = false;
			isPickingUp = true;
		}
	}
	
	void PickUp(){
		isPickingUp = false;
		isItemClose = false;
		isCarryingItem = true;
		carriedItem = itemClose;
		carriedItem.gameObject.GetComponent<itemScript>().isCarried = true;
	}
	
	public void Die(){
		if(!isDed){
			CheckItem();
			FallDown("death");
			isDed = true;
			//kidAnimator.SetTrigger("sleep");
			light.GetComponent<lightScript>().SetChildStance(isDed);
		}
	}

	public void SetCover(GameObject tree, bool cover)
	{
		if (!cover)
		{
			foreach (GameObject treeCovering in coveringTrees)
			{
				if (treeCovering == tree)
				{
					coveringTrees.Remove(treeCovering);
					if(coveringTrees.Count <= 0)
					{
						isCoveredByTree = false;
					}	
					break;
				}
			}
		}
		else
		{
			coveringTrees.Add(tree);
			isCoveredByTree = true;
		}
	}
	
	public void SpecialFollow(Vector2 target){
		specialTarget = target;
		followSpecialTarget = true;
		print("i start follow");
	}
	
	public void StopSpecialFollow(){
		followSpecialTarget = false;
	}
}
