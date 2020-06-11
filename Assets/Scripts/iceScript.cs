using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceScript : MonoBehaviour
{
	Collider2D light;
	bool isLighthere;
	[SerializeField] GameObject sprite;
	
	[SerializeField] public float meltTimer = 0.2f;
	[SerializeField] float meltTimerMax;
	[SerializeField] float hpToLoose = 0.05f;
	float hp = 1f;

	Animator iceAnimator;
	int state = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		iceAnimator = this.GetComponent<Animator>();

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);
		meltTimerMax = meltTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLighthere){
			if(light.GetComponent<lightScript>().isCharging){
				meltTimer -= Time.deltaTime;
				if(meltTimer <= 0){
					
					meltTimer = meltTimerMax;
					hp = hp - hpToLoose;
					state++;
                    

                    if (hp > 0){
						sprite.transform.localScale = new Vector3(hp,hp,0);
						iceAnimator.SetInteger("state", state);
					}else{
						Destroy(this.gameObject);
					}
                    gameObject.GetComponent<SonGlaceScript>().IceMelting(); //Sacha Place son bout de code là
                }
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			isLighthere = true;
			light = other;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			isLighthere = false;
		}
	}
}
