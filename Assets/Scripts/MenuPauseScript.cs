using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuPauseScript : MenuScript
{
    [SerializeField] Button btnContinue;
    [SerializeField] Button btnPause;
    [SerializeField] GameObject pnlPause;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space") || Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused)
                ContinueGame();
            else
                PauseGame();
        }
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        pnlPause.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(false);
        isPaused = true;
    }

    public void ContinueGame(){
        pnlPause.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        Application.LoadLevel("MainMenu");
        Time.timeScale = 1f;
    }

}
