using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWaterScript : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Kid")
        {
            other.GetComponent<kidScript>().isInWater = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Kid")
        {
            other.GetComponent<kidScript>().isInWater = false;
        }
    }
}
