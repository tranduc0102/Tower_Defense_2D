using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;
   [SerializeField] private Transform tf;
   public float bulletSpeed = 5f;
   private Transform target;
   private float damage;

   private void Update()
   {
      if (Mathf.Abs(tf.position.x)>10f)
      {
         PoolingManager.Despawn(gameObject);
      }
   }

   private void FixedUpdate()
   {
      if(!target) return;
      Vector2 dicrection = (target.position - transform.position).normalized;
      rb.velocity = dicrection * bulletSpeed;
   }

   public void SetTarget(Transform _target,float _damage)
   {
      this.target = _target;
      this.damage = _damage;
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Enemy"))
      {
         Monster monster = other.gameObject.GetComponent<Monster>();
         monster.TakeDamage(damage);
         PoolingManager.Despawn(gameObject);
      }
   }
}

