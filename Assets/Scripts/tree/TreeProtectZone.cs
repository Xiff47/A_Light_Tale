using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeProtectZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Kid")
        {
            other.GetComponent<kidScript>().SetCover(gameObject, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Kid")
        {
            other.GetComponent<kidScript>().SetCover(gameObject, false);
        }
    }
}
