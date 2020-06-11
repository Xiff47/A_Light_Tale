using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] protected Button btnRestart; // Pour commencer ou recommencer le jeu
    [SerializeField] protected Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        Application.LoadLevel ("Level0");
        Time.timeScale = 1f;
    }

    public void QuitGame(){
        print("Le jeu se termine.");
        Application.Quit();
    }
}
