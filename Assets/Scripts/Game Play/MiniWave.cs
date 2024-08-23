using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWave : MonoBehaviour
{
    public int miniWaveID;
    public Wave wave;
    public MiniWaveData miniWaveData;

    public List<MonsterData> listMonsterDatas;
    public List<Monster> listMonsters;
    public float spawnCoolDown;

    private Transform waveTrf;
    private Transform spawnerTrf;
    public Pathway pathWay;

    public void Init(MiniWaveData data)
    {
        miniWaveData = data;
        spawnCoolDown = data.spawnCooldown;

        var listMonsterID = data.listMonstersID;
        for (int i = 0; i < listMonsterID.Count; i++)
        {
            var monster = LevelManager.Instance.dataBase.listMonsterData[listMonsterID[i]];
            listMonsterDatas.Add(monster);
        }

        spawnerTrf = LevelManager.Instance.listSpawners[data.spawnerID].transform;
        waveTrf = LevelManager.Instance.listSpawners[data.spawnerID].transform;
        pathWay = LevelManager.Instance.listPathways[data.pathwayID];
        transform.SetParent(waveTrf);

        StartCoroutine(SpawnerMiniWave());
    }

    public IEnumerator SpawnerMiniWave()
    {
        for (int i = 0; i < listMonsterDatas.Count; i++)
        {
            SpawnEnermy(i);
            yield return new WaitForSeconds(spawnCoolDown);
        }
    }

    public void SpawnEnermy(int Idata)
    {
        var enermy = PoolingManager.Spawn(LevelManager.Instance.dataBase.listMonsterData[miniWaveData.listMonstersID[Idata]].monsterPrefab);
        enermy.name = listMonsterDatas[Idata].monsterName + " " + (Idata + 1);
        enermy.miniWave = this;
        enermy.IDIWave = Idata;
        enermy.transform.position = spawnerTrf.position;
        enermy.InitMonster(listMonsterDatas[Idata]);
        listMonsters.Add(enermy);
        enermy.transform.SetParent(transform);
    }

    public void checkIfAllEnermyDead()
    {
        if (listMonsters.Count == 0)
        {
            wave.listMiniWaves.Remove(this);
            wave.CheckIfAllMiniWaveClear();
            Destroy(gameObject);
            
        }
    }
}
