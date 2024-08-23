using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdateAndSale : MonoBehaviour
{
    public List<Sprite> srButtonUpdate;
    public Button updateButton;
    public GameObject imageOnUpdate;
    public GameObject imageUnUpdate;
    public Button saleButton;
    public GameObject tower;
    public TextMeshProUGUI textBuy;
    private int levelTower = 0;

    private void Start()
    {
        if (checkUpdate())
        {
            updateButton.onClick.AddListener(()=>UpdateTower());
        }
        saleButton.onClick.AddListener(()=>Sale());
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(unableUI());
        }
    }

    IEnumerator unableUI()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void UpdateTower()
    {
        if (checkUpdate())
        {
            if (LevelManager.Instance.SpriritStone >= LevelManager.Instance.dataBase.listTowerData[tower.GetComponent<Tower>()._towerID].listSpecifications[levelTower + 1].spiritStoneToBuy)
            {
                levelTower++;
                tower.GetComponent<Tower>().Specification(levelTower);
                LevelManager.Instance.SpriritStone -= tower.GetComponent<Tower>().spiritToBuy[levelTower];
            }
        }
        checkUpdate();
    }

    private void Sale()
    {
        LevelManager.Instance.SpriritStone += tower.GetComponent<Tower>().spiritToSale[levelTower];
        LevelManager.Instance.levelData.mapData.listTowerPositions.Add(transform.position);
        Destroy(tower);
        Destroy(gameObject);
    }

    private bool checkUpdate()
    {
        if (tower.GetComponent<Tower>().levelTower.Count > 1 && tower.GetComponent<Tower>().levelTower.Count -1 > levelTower)
        {
            updateButton.image.sprite = srButtonUpdate[0];
            imageOnUpdate.SetActive(true);
            imageUnUpdate.SetActive(false);
            textBuy.text =  LevelManager.Instance.dataBase.listTowerData[tower.GetComponent<Tower>()._towerID].listSpecifications[levelTower+1].spiritStoneToBuy.ToString();
            return true;
        }
        else
        {
            updateButton.image.sprite = srButtonUpdate[1];
            imageUnUpdate.SetActive(false);
            textBuy.text = null;
            imageUnUpdate.SetActive(true);
        }
        return false;
    }
}
