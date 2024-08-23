using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPosition : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject uiBuyPrefabs;
    private Vector2 target;
    private GameObject activeUI;

    void Start()
    {
        FindAllPlotObjects();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Day la UI");
                return;
            }
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);


            // Check for a valid tower position
            foreach (Vector2 validPosition in LevelManager.Instance.levelData.mapData.listTowerPositions)
            {
                if (Vector2.Distance(mousePosition, validPosition) < 0.5f)
                {
                    target = validPosition;
                    if (activeUI != null)
                    {
                        activeUI.SetActive(false);
                    }
                    activeUI = PoolingManager.Spawn(uiBuyPrefabs, target, quaternion.identity);
                    StartCoroutine(TimeDeActive());
                    break;
                }
            }
        }
    }

    private void FindAllPlotObjects()
    {
        GameObject[] plotObjects = GameObject.FindGameObjectsWithTag("Plot");
        LevelManager.Instance.levelData.mapData.listTowerPositions.Clear();
        if (LevelManager.Instance.levelData.mapData.listTowerPositions.Count == 0)
        {
            foreach (GameObject plotObject in plotObjects)
            {
                LevelManager.Instance.levelData.mapData.listTowerPositions.Add((Vector2)plotObject.transform.position);
            }   
        }
        else
        {
            return;
        }
    }

    IEnumerator TimeDeActive()
    {
        yield return new WaitForSeconds(2f);
        if (activeUI != null)
        {
            PoolingManager.Despawn(activeUI);
        }
    }
}