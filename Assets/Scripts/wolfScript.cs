using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfScript : MonoBehaviour
{
	Transform waypointManager;
	List<Vector2> waypointsPosition = new List<Vector2>();
	Vector2 retreatPosition;
	int waypointNumber;
	int currentWaypointTargeted;
	int maxWaypoint;

	kidScript kidScript;
	bool kidIsClose = false;

	bool isMoving = true;

	bool gettingMad = false;
	float gettingMadCountDown;
	[SerializeField] float gettingMadCountDownMax = 0f;
	bool isFleeing = false;
	bool isGoodBoi = false;
	float becomeGoodBoiTimerMax = 2f;
	float becomeGoodBoiTimer;
	float noMoreGoodBoiTimermax = 10f;
	float noMoreGoodBoiTimer;
	
	[SerializeField] float currentSpeed = 2f;

	Animator wolfAnimator;

    // Start is called before the first frame update
    void Start()
    {
		wolfAnimator = this.GetComponent<Animator>();
		wolfAnimator.SetFloat("direction", 2);

		waypointManager = transform.GetChild(0);
		foreach(Transform child in waypointManager.transform)
		{
			waypointsPosition.Add(new Vector2(child.position.x, child.position.y));
		}
		retreatPosition = waypointsPosition[0];
		// foreach(Transform child in waypointManager.transform)
		// {
			// Destroy(child.gameObject);
		// }

		gettingMadCountDown = gettingMadCountDownMax;
		maxWaypoint = waypointsPosition.Count-1;
		if (maxWaypoint > 0)
		{
			currentWaypointTargeted = 1;
		}
		else
		{
			currentWaypointTargeted = 0;
		}
		
		noMoreGoodBoiTimer = noMoreGoodBoiTimermax;
		becomeGoodBoiTimer = becomeGoodBoiTimerMax;
	}

    // Update is called once per frame
    void Update()
    {
		if(isGoodBoi){
			noMoreGoodBoiTimer -= Time.deltaTime;
			if(noMoreGoodBoiTimer <= 0){
				isGoodBoi = false;
				wolfAnimator.SetBool("calme", false);
				noMoreGoodBoiTimer = noMoreGoodBoiTimermax;
			}
			return;
		}
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);
	
		if (isMoving)
		{
			Vector3 vLouptoWP = waypointsPosition[currentWaypointTargeted] - (Vector2)transform.position;
			//transform.position = Vector3.MoveTowards(transform.position, waypointsPosition[currentWaypointTargeted], currentSpeed * Time.deltaTime);
			//Vector3 mouvement = new Vector3(new Vector3(waypointsPosition[currentWaypointTargeted].x, waypointsPosition[currentWaypointTargeted].y, 0) - transform.position)));
			//mouvement = new Vector3(mouvement.x, mouvement.y, 0);
			transform.position += (vLouptoWP.normalized * currentSpeed * Time.deltaTime);
			if(vLouptoWP.x > 0 && transform.localScale.x < 0 || vLouptoWP.x < 0 && transform.localScale.x > 0){
				transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);
			}
			float distanceToTravelLeft = Mathf.Sqrt(Mathf.Pow((waypointsPosition[currentWaypointTargeted].x - transform.position.x), 2) + Mathf.Pow((waypointsPosition[currentWaypointTargeted].y - transform.position.y), 2));
			if(distanceToTravelLeft <= 0.5)
			{
				if(currentWaypointTargeted < maxWaypoint)
				{
					currentWaypointTargeted++;
				}
				else
				{
					currentWaypointTargeted = 1;
				}
			}
		}
		else if (isFleeing)
		{
			//transform.position = Vector3.MoveTowards(transform.position, retreatPosition, (currentSpeed+2) * Time.deltaTime);
			float distanceToTravelLeft = Mathf.Sqrt(Mathf.Pow((retreatPosition.x - transform.position.x), 2) + Mathf.Pow((retreatPosition.y - transform.position.y), 2));
			transform.position += (Vector3)((retreatPosition-(Vector2)transform.position).normalized * (currentSpeed) * Time.deltaTime * distanceToTravelLeft);
			if (distanceToTravelLeft <= 0.1)
			{
				isFleeing = false;
				wolfAnimator.SetBool("fleeing", false);
				isMoving = true;
			}
		}
		else
		{
			isMoving = true;
		}

		if (kidIsClose && !isFleeing)
		{
			wolfAnimator.SetBool("grogne", true);
            gameObject.GetComponent<SonLoupScript>().PlayGrogneSound();//faire se jouer une fois

            switch (kidScript.GetCurrentSpeed())
			{
				case(1):
					isMoving = false;
					gettingMad = false;
					becomeGoodBoiTimer -= Time.deltaTime;
					if(becomeGoodBoiTimer <= 0){
						isGoodBoi = true;
						becomeGoodBoiTimer = becomeGoodBoiTimerMax;
                    }
					break;
				case(2):
					gettingMad = true;
					isMoving = false;
					becomeGoodBoiTimer = becomeGoodBoiTimerMax;
					// kid is jogging
					break;
				case (3):
					gettingMad = true;
					isMoving = false;
					becomeGoodBoiTimer = becomeGoodBoiTimerMax;
					// kid is running
					break;
				case (0):
					isMoving = false;
					gettingMad = false;
					becomeGoodBoiTimer -= Time.deltaTime;
					if(becomeGoodBoiTimer <= 0){
						isGoodBoi = true;
						print("Imma a good boi now !");
						wolfAnimator.SetBool("calme", true);
						becomeGoodBoiTimer = becomeGoodBoiTimerMax;
                        gameObject.GetComponent<SonLoupScript>().PlayGoodBoySound();
                    }
					break;
			}

			if (gettingMad)
			{
				gettingMadCountDown -= Time.deltaTime;
				if(gettingMadCountDown <= 0)
				{
					gettingMad = false;
					gettingMadCountDown = gettingMadCountDownMax;
					Bark();
				}
			}
		}
		else{
			wolfAnimator.SetBool("grogne", false);
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Kid" && !isFleeing){
			kidScript = other.GetComponent<kidScript>();
			kidIsClose = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Kid")
		{
			kidIsClose = false;
		}
	}

	public void Bark()
	{
		wolfAnimator.SetTrigger("bark");
		kidScript.Fallback(new Vector2(transform.position.x, transform.position.y), "wolf");
		isFleeing = true;
		wolfAnimator.SetBool("fleeing", true);
		kidIsClose = false;
		isMoving = false;
		print("OUAF");
        gameObject.GetComponent<SonLoupScript>().PlayBarkSound();

    }
}
