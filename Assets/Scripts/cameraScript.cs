using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
	[SerializeField] GameObject light;
	[SerializeField] GameObject kid;
	float KLgap;
	
	Vector3 lightPosition;
	float lightDistance;
	float distanceY;
	
	Vector3 mousePosition;
	float targetDistance;
	
	Vector3 target;
	
	[SerializeField] float speed;
	
	bool followLight = true;
	
	public float actualSize = 11f;
	float sizeTarget;
	float resizeTimer = 0.05f;
	float resizeTimerMax;
	
	float timerToWindDirection = 1f;
	float windDirection = 0.5f;
	float windDirectionTarget = 0.5f;
	int windSpeed = 100;
	
    // Start is called before the first frame update
    void Start()
    {
		GetComponent<Camera>().orthographic = true;
		sizeTarget = actualSize;
        if(speed == null || speed == 0){
			speed = 1;
		}
		lightPosition = new Vector3(light.transform.position.x, light.transform.position.y, light.transform.position.y-100);
		transform.position = lightPosition;
		
		transform.GetChild(0).transform.position += new Vector3(0,0,100f);
		
		resizeTimerMax = resizeTimer;
    }

    // Update is called once per frame
    void Update()
    {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition = new Vector3(mousePosition.x,mousePosition.y,0);
		
		lightPosition = new Vector3(light.transform.position.x, light.transform.position.y, light.transform.position.y-100);
		lightDistance = Mathf.Sqrt(Mathf.Pow((lightPosition.x - transform.position.x),2)+Mathf.Pow((lightPosition.y - transform.position.y),2));
		
		if(followLight){
			target = new Vector3(lightPosition.x, lightPosition.y, lightPosition.y-100);
		}
		
		targetDistance = Mathf.Sqrt(Mathf.Pow((target.x - transform.position.x),2)+Mathf.Pow((target.y - transform.position.y),2));
		distanceY = (Mathf.Abs(target.y - transform.position.y))/2+1;
		if(targetDistance>=0.2){
			transform.position = Vector3.MoveTowards(transform.position, target, speed * targetDistance * Time.deltaTime * distanceY);
		}
		if(actualSize < sizeTarget-0.5 || actualSize > sizeTarget+0.5){
			resizeTimer -= Time.deltaTime;
		}else{
			resizeTimer = resizeTimerMax;
		}
		if(resizeTimer <= 0){
			actualSize += (sizeTarget-actualSize)/100;
			GetComponent<Camera>().orthographicSize = actualSize;
			resizeTimer = resizeTimerMax;
		}
		
		SimulateSnowWind();
    }
	
	public void followTarget(Vector3 cameraTarget)
	{
		target = new Vector3(cameraTarget.x, cameraTarget.y, cameraTarget.y-100);
		followLight = false;
	}
	
	public void setFollowLight()
	{
		followLight = true;
	}
	
	public void SetZoom(float v){
		sizeTarget = v;
	}
	
	private void SimulateSnowWind(){
		timerToWindDirection -= Time.deltaTime;
		if(timerToWindDirection <= 0){
			timerToWindDirection = Random.Range(0.9f, 5f);
			windDirectionTarget = Random.Range(-10f,10f);
			windSpeed = Random.Range(10,101);
		}
		float step = (windDirectionTarget-windDirection)/windSpeed;
		if(step > 0.001 || step < 0.001){
			windDirection += step;
		}
		ParticleSystem.VelocityOverLifetimeModule snowVelocity = transform.GetChild(0).GetComponent<ParticleSystem>().velocityOverLifetime;
		//And to modify the value
		ParticleSystem.MinMaxCurve rate = new ParticleSystem.MinMaxCurve();
		
		rate.constantMax = windDirection; // or whatever value you want
		snowVelocity.x = rate;
	}
}
