using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Game Data",menuName = "Data/Game Data")]
public class DataBase : ScriptableObject
{
    public Tower towerPrefab;
    public PrefabData prefabData;
    public List<MonsterData> listMonsterData;
    public List<TowerData> listTowerData;
}

[Serializable]
public class TowerData
{
    public string towerName;
    public int towerID;
    public bool canAirShoot;
    public List<TurretSpecification> listSpecifications;
}

[Serializable]
public class TurretSpecification //thong so thap
{
    public string name;
    public int spiritStoneToBuy;// da linh hon can de mua
    public int spiritStoneGetWhenSale;// da linh hon khi ban
    public float damage;
    public Sprite caseSprite;
    public Sprite towerSprite;// anh thap
    public Bullet bulletPrefab;//dan
    public float cooldown;
    public float shootingRange;// khoang ban

}

[Serializable]
public class MonsterData //Thong so quai
{
    public string monsterName;
    public int monsterID;
    public int sprititStoneAmount;
    public int damage;
    public Monster monsterPrefab;
    public float maxHP;
    public float speed = 2f;
}
[Serializable]
public class PrefabData 
{
    public Spawner spawnerPrefab;
    public Pathway pathwayPrefab;//duong di
    public Wave wavePrefab;// dot quai
    public MiniWave miniWavePrefab;// dot quai nho
    public TowerPosition towerPosition;//vi tri thap
}