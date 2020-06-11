using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class scriptedEvent7_changeScene : MonoBehaviour
{
	
	bool started = false;
	[SerializeField] GameObject canvas;
	[SerializeField] float timer = 3;
	[SerializeField] string sceneToLoadString = null;
	float t;
	
    // Start is called before the first frame update
    void Start()
    {
        t = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(started){
			FadeScreenToWhite();
		}
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			started=true;
		}
	}
	
	void FadeScreenToWhite(){
		float visibility = 1-(t/timer);
		canvas.transform.GetChild(0).GetComponent<Image>().color = new Color(255,255,255, visibility);
		t -= Time.deltaTime;
		if(t<=0){
			ChangeScene();
			print("ping");
		}
	}
	
	void ChangeScene(){
		if(sceneToLoadString != null){
			SceneManager.LoadScene("Scenes/"+sceneToLoadString);
			print("reping");
		}else{
			print("You gonna have a bad time");
		}
	}
}
