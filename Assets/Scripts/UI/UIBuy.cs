using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UIBuy : MonoBehaviour
{
    [Header("-----Buy-----")]
    private GameObject buy;
    public Button buyMachine;
    public Button buyCanon;
    public Button buyRocket;
    public GameObject tower;

    private void Start()
    {
        buy = gameObject;
    }

    private void OnEnable()
    {
        buyMachine.onClick.AddListener(() => buyTower(100, 0));
        buyCanon.onClick.AddListener(() => buyTower(200, 1));
        buyRocket.onClick.AddListener(() => buyTower(300, 2));
    }
    

    public void buyTower(int amount, int ID)
    {
        if (LevelManager.Instance.SpriritStone >= amount && LevelManager.Instance.levelData.mapData.listTowerPositions.Count > 0)
        {
            LevelManager.Instance.SpriritStone -= amount;
            SpawnTower(ID);
        }
        buy.SetActive(false);
    }
    
    private void SpawnTower(int ID)
    {
        if (LevelManager.Instance.levelData.mapData.listTowerPositions.Count == 0) return;
        
        Vector2 spawnPosition = transform.position;
        if (LevelManager.Instance.levelData.mapData.listTowerPositions.Contains(spawnPosition))
        {
            GameObject newTower = Instantiate(tower, spawnPosition, quaternion.identity);
           newTower.GetComponent<Tower>()._towerID = ID;
           LevelManager.Instance.levelData.mapData.listTowerPositions.Remove(spawnPosition);
       }
    }
}
