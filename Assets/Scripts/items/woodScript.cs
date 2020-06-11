using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodScript : MonoBehaviour
{
	
    Animator woodAnimator;

    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<itemScript>().SetType("wood");
      woodAnimator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnMouseOver()
    {
      woodAnimator.SetBool("mouseOver", true);
    }

    void OnMouseExit()
    {
      woodAnimator.SetBool("mouseOver", false);
    }
}
