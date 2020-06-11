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
    public GameObject light;
    public GameObject kid;
	int activationInt = 0;

    void Start()
    {
        //Initialise the Vector
        m_MyFirstVector = Vector2.zero;
        m_Angle = 0.0f;
		transform.GetChild(0).gameObject.SetActive(false);
    }

    void Update()
    {
        //Fetch the first GameObject's position
        m_MyFirstVector = Vector2.up;
        //Fetch the second GameObject's position
        m_MySecondVector = (light.transform.position-kid.transform.position).normalized;
        //Find the angle for the two Vectors
        m_Angle = Vector2.SignedAngle(m_MyFirstVector, m_MySecondVector);
		if(true){
			UpdateUI();
		}else{
			gameObject.SetActive(false);
		}
		
		if (Input.GetKeyUp("n"))
        {
            ToggleVisibility();
        }
    }
	
	void UpdateUI(){
		transform.eulerAngles = new Vector3(
			0,
			0,
			m_Angle
		);
	}
	
	void ToggleVisibility(){
		activationInt = (activationInt+1)%3;
		transform.GetChild(0).gameObject.SetActive(activationInt==2);
	}
}
