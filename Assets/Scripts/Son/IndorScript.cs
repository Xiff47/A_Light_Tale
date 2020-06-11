﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndoorScript : MonoBehaviour
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
        if (other.gameObject.tag == "kid")
        {
            other.gameObject.GetComponent<kidScript>().isIndoor = true;
        }
    }
    void OnExitEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "kid")
        {
            other.gameObject.GetComponent<kidScript>().isIndoor = false;
        }
    }
}