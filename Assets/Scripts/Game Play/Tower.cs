using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class Tower : MonoBehaviour
{ 
    
    [Header("TowerData")]
    public string _towerName; 
    public int _towerID;
    public List<TurretSpecification> _listSpecifications;

    [Header("Tower")] 
    public SpriteRenderer caseSprite;
    public SpriteRenderer towerSprite;
    public  float damage;
    public  float arrangeShooting;
    public  double cooldown;
    public  GameObject bulletPrefab;
    public List<int> levelTower;
    public List<int> spiritToBuy;
    public List<int> spiritToSale;

    [Header("UI Update and Sale")] public GameObject uiUpdateAndSale;


    private void Start()
    {
        Init(LevelManager.Instance.dataBase.listTowerData[_towerID]);
        levelTower = LevelManager.Instance.levelData.towerInLevel[_towerID].towerAllowed;
        Specification(0);
    }
    

    public void Init(TowerData data)
    {
        _towerName = data.towerName;
        _listSpecifications = data.listSpecifications;
    }

    public void Specification(int amount)
    {
        spiritToBuy.Add( _listSpecifications[amount].spiritStoneToBuy);
        spiritToSale.Add(_listSpecifications[amount].spiritStoneGetWhenSale);
        caseSprite.sprite = _listSpecifications[amount].caseSprite;
        towerSprite.sprite = _listSpecifications[amount].towerSprite;
        damage = _listSpecifications[amount].damage;
        cooldown = _listSpecifications[amount].cooldown;
        arrangeShooting = _listSpecifications[amount].shootingRange;
        bulletPrefab = _listSpecifications[amount].bulletPrefab.gameObject;
    }
    GameObject uI = null;
    private void OnMouseDown()
    {
        if (uI == null)
        {
            uI = PoolingManager.Spawn(uiUpdateAndSale, gameObject.transform.position, quaternion.identity);
            uI.GetComponent<UIUpdateAndSale>().tower = gameObject;
        }
        else
        {
            uI.SetActive(true);
        }
        GameObject childObject = transform.GetChild(1).gameObject;
        TowerCollider towerCollider = childObject.GetComponentInChildren<TowerCollider>(true);

        if (towerCollider != null)
        {
            towerCollider.timeX = 5f;
        }
    }

    IEnumerator unableUI()
    {
        yield return new WaitForSeconds(1f);
        uI.SetActive(false);
    }
}
