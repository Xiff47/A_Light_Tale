﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSonEnfant : MonoBehaviour
{
    bool isPlaying = false;
    bool hasPlayed = false;
    [SerializeField] int ValueTableau = 0;

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
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SonEnfantScript>().PlaySpecificSound(ValueTableau);
        }
    }
}
