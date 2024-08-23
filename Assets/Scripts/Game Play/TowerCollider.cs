using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TowerCollider : MonoBehaviour
{
    [SerializeField] private Transform rotationPoint;
    public Tower _tower;
    private Transform target;
    public List<Transform> listEnemy = new List<Transform>();
    public double timeSpawn;
    public Transform poitSpawn;
    public SpriteRenderer sr;
    public float timeX = 5f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
    }

    private void Update()
    {
        if (timeX <= 0)
        {
            sr.enabled = false;
        }
        else
        {
            timeX -= Time.deltaTime;
            sr.enabled = true;
        }
        transform.localScale = new Vector3(_tower.arrangeShooting, _tower.arrangeShooting,0f);
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        
        RotateTowardTarget();
        timeSpawn += Time.deltaTime;
        if (timeSpawn >= _tower.cooldown)
        {
            SpawnBullet();
        }
    }

    private void FindTarget()
    {
        if (listEnemy.Count > 0)
        {
            target = listEnemy[0];
        }
    }

    private void RotateTowardTarget()
    {
        if (target == null) return;

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, 300f * Time.deltaTime);
    }

    private void SpawnBullet()
    {
        GameObject bullet = PoolingManager.Spawn(_tower.bulletPrefab, poitSpawn.position, quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.transform.rotation = rotationPoint.rotation;
        bulletScript.SetTarget(target, _tower.damage);
        timeSpawn = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            listEnemy.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (listEnemy.Contains(other.transform))
            {
                listEnemy.Remove(other.transform);
                target = null;
            }
        }
    }

  
}
