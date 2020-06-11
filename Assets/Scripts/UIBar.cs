using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBar : MonoBehaviour
{
	
	[SerializeField] GameObject bar;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetSize(float sizeNormalised){
		bar.transform.localScale = new Vector3(sizeNormalised*500, 50f);
	}
}
