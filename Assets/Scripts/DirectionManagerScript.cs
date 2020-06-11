using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionManagerScript : MonoBehaviour
{
	//Use these to get the GameObject's positions
    Vector2 m_MyFirstVector;
    Vector2 m_MySecondVector;

    float m_Angle;
	int activationInt = 0;
    //You must assign to these two GameObjects in the Inspector
    public GameObject light;
    public GameObject kid;
	bool toggleOn;

    void Start()
    {
        //Initialise the Vector
        m_MyFirstVector = Vector2.zero;
        m_Angle = 0.0f;
		foreach (Transform child in transform){
			child.gameObject.SetActive(false);
		}
		toggleOn = false;
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
		foreach (Transform child in transform){
			child.gameObject.SetActive(false);
		}
		if(!toggleOn){
			return;
		}
		
		if(m_Angle <= -157.5){
			ManagePointer(transform.Find("North").gameObject);
		}else if(m_Angle <= -112.5){
			ManagePointer(transform.Find("NorthWest").gameObject);
		}else if(m_Angle <= -67.5){
			ManagePointer(transform.Find("West").gameObject);
		}else if(m_Angle <= -22.5){
			ManagePointer(transform.Find("SouthWest").gameObject);
		}else if(m_Angle <= 22.5){
			ManagePointer(transform.Find("South").gameObject);
		}else if(m_Angle <= 67.5){
			ManagePointer(transform.Find("SouthEast").gameObject);
		}else if(m_Angle <= 112.5){
			ManagePointer(transform.Find("East").gameObject);
		}else if(m_Angle <= 157.5){
			ManagePointer(transform.Find("NorthEast").gameObject);
		}else{
			ManagePointer(transform.Find("North").gameObject);
		}
	}
	
	void ManagePointer(GameObject pointer){
		pointer.SetActive(true);
	}
	
	void ToggleVisibility(){
		activationInt = (activationInt+1)%3;
		toggleOn = (activationInt==1);
	}
}
