using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Monster : MonoBehaviour
{
   public MiniWave miniWave;
   [SerializeField] private MonsterData monsterData;
   private Rigidbody2D rb;
   [SerializeField] private float curHP;

   public HealthBar healthBar;
   public Vector2 target;
   public int pathIndex = 0;
   public int IDIWave;
   private int spiritStoneAmount;
   private int damage;

   private float notTakeDamageTime = 0f;
   private float timeToHideHealthBar = 2;

   public void InitMonster(MonsterData data)
   {
      monsterData = data;
      curHP = data.maxHP;
      spiritStoneAmount = data.sprititStoneAmount;
      damage = data.damage;
      rb = GetComponent<Rigidbody2D>();
      healthBar = GetComponentInChildren<HealthBar>();
      healthBar.SetMaxHP(data.maxHP);
   }

   private void Start()
   {
      target = miniWave.pathWay.wayPoint[0];
      healthBar.gameObject.SetActive(false);
   }

   private void Update()
   {
      notTakeDamageTime += Time.deltaTime;
      if (notTakeDamageTime >= timeToHideHealthBar)
      {
         healthBar.gameObject.SetActive(false);
      }

      if (Vector2.Distance(target, transform.position) <= 0.1f)
      {
         pathIndex++;
      }

      if (pathIndex == miniWave.pathWay.wayPoint.Count)
      {
         this.PostEvent(EventID.On_Monster_Escaped,damage);
         miniWave.listMonsters.Remove(this);
         miniWave.checkIfAllEnermyDead();
         Destroy(gameObject);
      }
   }

   private void FixedUpdate()
   {
      Vector2 direction = ((Vector3)target - transform.position).normalized;
      rb.velocity = direction * monsterData.speed;
   }

   public void TakeDamage(float amount)
   {
      healthBar.gameObject.SetActive(true);
      notTakeDamageTime = 0f;
      curHP -= amount;
      if (curHP <= 0f)
      {
         OnMonsterDie();
      }
      healthBar.SetHP(curHP);
   }

   public void OnMonsterDie()
   {
      miniWave.listMonsters.Remove(this);
      miniWave.checkIfAllEnermyDead();
      this.PostEvent(EventID.On_Monster_Killed,spiritStoneAmount);
      Destroy(gameObject);
   }
}
