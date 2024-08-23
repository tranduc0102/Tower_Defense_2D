using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Button hackBtn;
    public Button pauseBtn;
    public Button speedBtn;
    public TextMeshProUGUI spiritStioneText;
    public TextMeshProUGUI liveLeftText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI waveNameText;
    public TextMeshProUGUI speedText;

    private int timeScale;
    private bool isPause = false;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite resumeSprite;

    [Header("UI Pause")] 
    public GameObject panel;
    public Button btnHome;
    public Button btnReplay;
    private void Awake()
    {
        Time.timeScale = timeScale = 1;
        speedText.text = "X" + Time.timeScale;
        spiritStioneText.text = "0";
        liveLeftText.text = "0";
        waveNameText.text = "Wave 1";
        waveNameText.alpha = 0f;
        waveNumberText.text = "1/" + LevelManager.Instance.levelData.listWavesData.Count;
    }

     private void OnEnable()
     {
          this.RegisterListener(EventID.On_Spirit_Stone_Change, param => UpdateSpiritStione((int) param));
          this.RegisterListener(EventID.On_Lives_Change,param => UpdateLives((int)param));
          this.RegisterListener(EventID.On_Monster_Killed,param =>UpdataSpiritStoneWhileEnemyDead((int)param));
          this.RegisterListener(EventID.On_Monster_Escaped,param=>UpdateLiveWhenEmenyAttack((int)param));
         // this.RegisterListener(EventID.On_Player_Win,param=>HandlePlayerWin((int)param));
         // this.RegisterListener(EventID.On_Player_Lose,param=>HandlePlayerLose((int)param));
         //
         hackBtn.onClick.AddListener(() => { 
             LevelManager.Instance.SpriritStone += 1000; 
             LevelManager.Instance.Lives += 5; });
         pauseBtn.onClick.AddListener(() => PauseGame());
         speedBtn.onClick.AddListener(() => ChangGameSpeed());
         btnHome.onClick.AddListener((() => SceneManager.LoadSceneAsync("Lobby")));
         btnReplay.onClick.AddListener((() => SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name)));
     }
     
     private void OnDisable()
     {
         //this.RemoveListener(EventID.On_Spirit_Stone_Change, param => UpdateSpiritStione((int) param));
         //this.RemoveListener(EventID.On_Lives_Change,param => UpdateLive((int)param);
         //this.RemoveListener(EventID.On_Player_Win,param=>HandlePlayerWin((int)param));
         //this.RemoveListener(EventID.On_Player_Lose,param=>HandlePlayerLose((int)param));
     }
     public void UpdateSpiritStione(int amount)
     {
         spiritStioneText.text = amount.ToString();
     }

     public void UpdataSpiritStoneWhileEnemyDead(int amount)
     {
         LevelManager.Instance.SpriritStone += amount;
     }

     public void UpdateLiveWhenEmenyAttack(int amount)
     {
         if (LevelManager.Instance.Lives > 0)
         {
             LevelManager.Instance.Lives -= amount;   
         }
     }

     public void UpdateLives(int amount)
     {
         liveLeftText.text = amount.ToString();
     }

     public IEnumerator ShowWaveName(int waveID)
     {
         yield return new WaitForSeconds(1f);
         waveNameText.text = "Wave " + (waveID + 1);
         waveNumberText.text = (waveID + 1) + "/" + LevelManager.Instance.levelData.listWavesData.Count;
         waveNameText.DOFade(1f, 0.5f);
         yield return new WaitForSeconds(2f);
         waveNameText.DOFade(0f, 0.5f);

         yield return new WaitForSeconds(1f);
         LevelManager.Instance.CreateWave(waveID);
     }

     public void ResumeGame()
     {
         Time.timeScale = timeScale;
         isPause = false;
         pauseBtn.image.sprite = pauseSprite;
     }

     private void PauseGame()
     {
         if (!isPause)
         {
             Time.timeScale = 0f;
             isPause = true;
             pauseBtn.image.sprite = resumeSprite;
             panel.SetActive(true);
         }
         else
         {
             panel.SetActive(false);
             ResumeGame();
         }
     }

     private void ChangGameSpeed()
     {
         switch (timeScale)
         {
             case 1:
                 timeScale = 2;
                 break;
             case 2:
                 timeScale = 4;
                 break;
             default:
                 timeScale = 1;
                 break;
         }

         speedText.text = "X" + timeScale;
         if (!isPause)
         {
             Time.timeScale = timeScale;
         }
     }
}
