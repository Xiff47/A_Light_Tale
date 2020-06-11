using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __specialPositionYandZ : MonoBehaviour
{
	[SerializeField] float offset = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y/100+offset);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
