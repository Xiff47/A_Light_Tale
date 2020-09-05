using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWaterScript : MonoBehaviour
{
    public bool waterFootstep;

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
        if (other.gameObject.tag == "Kid")
        {
            waterFootstep = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Kid")
        {
            waterFootstep = false;
        }
    }
}
