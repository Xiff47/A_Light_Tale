using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndorScript : MonoBehaviour
{
    public bool isindor = false;

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
        while (other.gameObject.tag == "Player")
        {
            isindor = true;

            //est a l'interieur
        }
    }
}