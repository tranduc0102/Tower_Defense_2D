using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    [Header("UI Lobby")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSetting;
    [SerializeField] private Button btnInfo;
  
    [Header("Active UI when click button in Lobby")]
    public GameObject panelLevel;
    public GameObject panelInfo;
    //public GameObject panelSetting;
    [Header("UI panel Play")] 
    public Button btnLevel1; 
    public Button btnLevel2; 
    public Button btnLevel3;
    public Button btnClose1;
    [Header("UI info")]
    public Button btnClose2;

    private void Start()
    {
        btnPlay.onClick.AddListener(()=> panelLevel.SetActive(true));
        btnExit.onClick.AddListener(()=>Application.Quit());
        //btnSetting.onClick.AddListener(()=>panelSetting.SetActive(true));
        btnInfo.onClick.AddListener(()=>panelInfo.SetActive(true));
        
        
        btnLevel1.onClick.AddListener(()=>SceneManager.LoadSceneAsync("Level_1"));
        btnLevel2.onClick.AddListener(()=>SceneManager.LoadSceneAsync("Level_2"));
        btnLevel3.onClick.AddListener(()=>SceneManager.LoadSceneAsync("Level_3"));
        btnClose1.onClick.AddListener(() => panelLevel.SetActive(false));
        
        
        btnClose2.onClick.AddListener(() => panelInfo.SetActive(false));
    }
}
