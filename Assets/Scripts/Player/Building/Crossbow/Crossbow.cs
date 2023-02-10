using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    #region Parameters

    [SerializeField] private GameObject dart;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float shootInterval;
    [SerializeField] private float speedBulletModifier;
    private Transform components;
    private Transform parentSpawner;
    private Animator anim;

    private Transform nearEnemy;
    private float nearEnemyDist;
    private float lastShoot;
    private bool shooting;
    
    #endregion

    private void Start()
    {
        parentSpawner = SpawnManager.parentSpawner;
        anim = GetComponent<Animator>();
        components = transform.GetChild(1);
    }

    private void Update()
    {
        if (nearEnemy != null)  UpdateRotate();
    }

    private void FixedUpdate()
    {
        nearEnemy = NearEnemy();
        if (nearEnemy != null) nearEnemyDist = (nearEnemy.position - transform.position).magnitude;
        else nearEnemyDist = 999;
        
        if (lastShoot < shootInterval) lastShoot += Time.fixedDeltaTime;
        else if(nearEnemyDist < 10 && !shooting)
        {
            shooting = true;
            anim.Play("Shooting");
            Invoke("RequestShoot",0.5f);
        }
    }

    private Transform NearEnemy()
    {
        Transform newNearEnemy = DayController.dayController.gameObject.transform; //ponto distante
        float newNearEnemyDist = (newNearEnemy.position - transform.position).magnitude;
        
        for (int i = 1; i < parentSpawner.childCount; i++)
        {
            for (int j = 0; j < parentSpawner.GetChild(i).childCount; j++)
            {
                if (parentSpawner.GetChild(i).GetChild(j).gameObject.activeSelf &&
                    (parentSpawner.GetChild(i).GetChild(j).position - transform.position).magnitude < newNearEnemyDist)
                {
                    newNearEnemyDist = (parentSpawner.GetChild(i).GetChild(j).position - transform.position).magnitude;
                    newNearEnemy = parentSpawner.GetChild(i).GetChild(j);
                }
            }
        }
        if (newNearEnemy == DayController.dayController.gameObject.transform) return null;
        return newNearEnemy;
    }
    
    public void RequestShoot()
    {
        lastShoot = 0;
        
        GameObject bullet = BulletPool.bulletPool.DeliveryBullet();
        bullet.GetComponent<Dart>().owner = gameObject;
        bullet.transform.rotation = components.rotation;
        float angle = components.rotation.eulerAngles.z+90;
        Vector3 dir = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad),0);
        bullet.transform.position = transform.position + dir.normalized * 2;
        
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().velocity = dir * speedBulletModifier;
        anim.Play("Idle");
        shooting = false;
    }

    private void UpdateRotate()
    {
        Vector3 direction = nearEnemy.position - components.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        Vector3 targetRotation = new Vector3(0, 0, angle-90);
        components.rotation = (Quaternion.Lerp(components.rotation, Quaternion.Euler(targetRotation), PlayerStatus.status.GetRotateModifier()));
    }
}
