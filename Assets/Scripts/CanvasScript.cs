using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
	GameObject healthBar;
	bool isHealthBarActive;
	GameObject lightBar;
	bool isLightBarActive;
	
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar");
		isHealthBarActive = false;
		healthBar.SetActive(isHealthBarActive);
		lightBar = GameObject.Find("LightBar");
		isLightBarActive = false;
		lightBar.SetActive(isLightBarActive);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp("b"))
        {
            ToggleBarVisibility();
        }*/
		
    }
	
	void ToggleBarVisibility(){
		isHealthBarActive = !isHealthBarActive;
		healthBar.SetActive(isHealthBarActive);
		isLightBarActive = !isLightBarActive;
		lightBar.SetActive(isLightBarActive);
	}
}
