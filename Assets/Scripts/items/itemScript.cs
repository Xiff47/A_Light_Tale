using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
	public bool isCarried;
	public string type = "null";
	
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetType(string newType){
		type = newType;
	}
}
