using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MenuMainScript : MenuScript
{
    [SerializeField] protected Button btnBack;
    [SerializeField] GameObject pnlCredits;
    [SerializeField] GameObject pnlControles;
    //[SerializeField] protected Button btnControles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayPanelCredits()
    {
        pnlCredits.gameObject.SetActive(!pnlCredits.gameObject.activeSelf);
    }

    public void DisplayPanelControles()
    {
        pnlControles.gameObject.SetActive(!pnlControles.gameObject.activeSelf);
    }
}
