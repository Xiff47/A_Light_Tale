using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boussoleScript : MonoBehaviour
{
    //Use these to get the GameObject's positions
    Vector2 m_MyFirstVector;
    Vector2 m_MySecondVector;

    float m_Angle;

    //You must assign to these two GameObjects in the Inspector
    GameObject light;
    GameObject kid;
	bool isVisible;
	public int visibleState = 0;
	float t = 0;
	float timer = 1f;
	public float visibility = 0;
	bool started = false;
	
    void Start()
    {
        //Initialise the Vector
        m_MyFirstVector = Vector2.zero;
        m_Angle = 0.0f;
		light = transform.parent.gameObject;
		kid = light.GetComponent<lightScript>().kid;
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255,255,255, 0);
    }

    void Update()
    {
        //Fetch the first GameObject's position
        m_MyFirstVector = Vector2.up;
        //Fetch the second GameObject's position
        m_MySecondVector = (light.transform.position-kid.transform.position).normalized;
        //Find the angle for the two Vectors
        m_Angle = Vector2.SignedAngle(m_MyFirstVector, m_MySecondVector);
		if(started){
			UpdateUI();
			switch(visibleState){
				case 0 :
					visibility = (t/timer);
					t += Time.deltaTime;
					if(t>=timer){
						visibleState++;
						timer = 1;
						t = 0;
					}
				break;
				case 1 :
					visibility = (t/timer);
					transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255,255,255, visibility);
					if(!isVisible && t>=0){
						t -= Time.deltaTime;
						if (t <= 0)
						{
							visibleState = 0;
							t = 0;
							timer = 1f;
							visibility = 0;
							started = false;
						}
					}else{
						t += Time.deltaTime;
					}
					if(t>=timer){
						visibleState++;
						t=0;
						timer=0.2f;
					}
				break;
				case 2 :
					if(!isVisible){
						visibility = 1-(t/timer);
						transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255,255,255, visibility);
						t += Time.deltaTime;
						if(t>=timer){
							visibleState=0;
							started = false;
							timer = 1;
							t = 0;
						}
					}
				break;
			}
		}else{
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255,255,255, 0);
		}
    }
	
	void UpdateUI(){
		transform.eulerAngles = new Vector3(
			0,
			0,
			m_Angle
		);
	}
	
	public void SetVisibility(bool isFarAway){
		if(isFarAway && visibleState == 0){
			isVisible = true;
			started = true;
		}
		if(!isFarAway){
			isVisible = false;
			if(visibleState == 0){
				started = false;
			}
		}
	}
}
