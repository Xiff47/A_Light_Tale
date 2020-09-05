using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSnowfallScript : MonoBehaviour
{
    bool isFalling = false;
    bool hasFell = false;
    float fallTimer = 3;
   

    bool isKidInside = false;

    kidScript kid;

    Animator snowTreeAnimator;

	[SerializeField] GameObject snow;
    // Start is called before the first frame update
    void Start()
    {
        snowTreeAnimator = GameObject.Find("Snowfall").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling && !hasFell)
        {
            snowTreeAnimator.SetInteger("state", 1);
            fallTimer -= Time.deltaTime;
            if(fallTimer <= 0)
            {
                snow.transform.position = transform.parent.transform.position;
                snowTreeAnimator.SetInteger("state", 2);
                gameObject.GetComponent<SonSnowFallScript>().PlaySnowFallSound();
                if (isKidInside)
                {
                    kid.Fallback("tree");
					isFalling = false;
					hasFell = true;
                }
            }
        }
    }
 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Kid")
        {
            isFalling = true;
            isKidInside = true;
            kid = other.transform.GetComponent<kidScript>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Kid")
        {
            isKidInside = false;
        }
    }
}
