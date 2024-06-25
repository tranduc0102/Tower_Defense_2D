using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public GameData gameData;
    public LevelData levelData;
    public DataBase dataBase;

    public Transform spawnersTrf;
    public Transform pathWaysTrf;
    public Transform plotTrf;

    public List<Wave> listWaves = new List<Wave>();
    public List<Spawner> listSpawners = new List<Spawner>();
    public List<Pathway> listPathways = new List<Pathway>();

    private int spriritStone = 0;
    private int lives = 0;

    public int SpriritStone
    {
        get => spriritStone;
        set
        {
            spriritStone = value;
            //this.postEvent(EventID.On_spr
        }
    }

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
        }
    }

    protected void Awake()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        SpriritStone = levelData.spiritStoneStart;
        Lives = levelData.liveStart;
        CreateSpawnersAndPathways();
    }

    public void CreateSpawnersAndPathways()
    {
        for (int i = 0; i < levelData.layoutData.spawnersData.Count; i++)
        {
            var spawner = Instantiate(dataBase.prefabData.spawnerPrefab, spawnersTrf);
            spawner.spawnerID = i;
            spawner.InitSpawner(levelData.layoutData.spawnersData[i]);
            listSpawners.Add(spawner);
        }
        
        for (int i = 0; i < levelData.layoutData.pathwaysData.Count; i++)
        {
            var pathway = Instantiate(dataBase.prefabData.pathwayPrefab, pathWaysTrf);
            pathway.pathwayID = i;
            pathway.InitPathway(levelData.layoutData.pathwaysData[i]);
            listPathways.Add(pathway);
        }
    }
    
    //sinh quai cho wave
    public void CreateWave()
    {
        
    }
}
