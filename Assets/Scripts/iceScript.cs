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
	[SerializeField] float hpToLoose = 0.125f; // Change en fonction du nombre de frame du bloc de glace
	float hp = 1f;

	Animator iceAnimator;
	int state = 0;
	bool isMelting = false;
	bool meltSound;
	
    // Start is called before the first frame update
    void Start()
    {
		iceAnimator = this.GetComponent<Animator>();

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100);
		meltTimerMax = meltTimer;
		gameObject.GetComponent<SonGlaceScript>();

	}

    // Update is called once per frame
    void Update()
    {
        if(isLighthere){
			if(light.GetComponent<lightScript>().isCharging){
				meltTimer -= Time.deltaTime;
				isMelting = true;
				iceAnimator.SetBool("isMelting", true);
				if(meltTimer <= 0){
					
					meltTimer = meltTimerMax;
					hp = hp - hpToLoose;
					state++;
					print("State = " + state + " // hp = " + hp + " // hpToLoose = " + hpToLoose);
					

					if (hp > 0){
						sprite.transform.localScale = new Vector3(hp,hp,0);
						iceAnimator.SetInteger("state", state);
					}else{
						Destroy(this.gameObject);
						meltSound = true;
					}
					if (meltSound == false)
					{
						gameObject.GetComponent<SonGlaceScript>().IceMelting();
						meltSound = true;
					}
                }
			}else{
				isMelting = false;
				iceAnimator.SetBool("isMelting", false);
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
