using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonMusicScript : MonoBehaviour
{
	bool MisPlaying = false;
	bool MhasPlayed = false;
	AudioSource MaudioSource;

	// Start is called before the first frame update
	void Start()
	{
		MaudioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (MhasPlayed == false)
			{
				MisPlaying = true;
				MaudioSource.Play();
				MhasPlayed = true;
			}
		}
	
	}
}

