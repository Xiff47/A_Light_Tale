using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footstepScript : MonoBehaviour
{
	
	float lifespan = 15;
	float opacity = 1;
	float beginFadeaway = 10;

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;
		if(lifespan <= beginFadeaway){
			opacity = 1*(lifespan/beginFadeaway);
		}
		this.GetComponent<SpriteRenderer>().color = new Color(200,200,200,opacity);
		if(lifespan<=0){
			Destroy(this.gameObject);
		}
    }
}
