using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSonVolume : MonoBehaviour
{

    bool hasPlayed2 = false;

    [SerializeField] int ValueTableau2 = 0;

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
        if (hasPlayed2 == false)
        {

            other.gameObject.GetComponent<SonEnfantScript>().PlaySpecificSoundVolume(ValueTableau2);
            print("ca joue?");
            hasPlayed2 = true;
        }
    }
}
