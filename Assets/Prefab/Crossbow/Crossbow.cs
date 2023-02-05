using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : MonoBehaviour
{
    
    // Variáveis para o alcance da torre e o tempo de recarga entre disparos
    public float range = 10.0f;
    public float fireRate = 1.0f;

    // Variáveis para armazenar o projétil e o tempo do último disparo
    public GameObject projectile;
    private float lastFireTime;

    // Variável para armazenar o inimigo mais próximo
    private GameObject nearestEnemy;

    private bool canShoot = false;
    private bool canMove = false;

    private void Start()
    {
        StartCoroutine(CrossbowConstruct());
    }
    
    IEnumerator CrossbowConstruct()
    {
        yield return new WaitForSeconds(10f);
        GetComponent<ParticleSystem>().Stop();
        canMove = true;

    }

    void Update()
    {
        if (!canMove) return;
        
        // Atualiza o inimigo mais próximo
        nearestEnemy = FindNearestEnemy();

        if (nearestEnemy != null) RotateTowardsEnemy();

        // Se houver um inimigo na distância de alcance e o tempo de recarga tiver passado, rotacione e atire
        if (nearestEnemy != null && Time.time >= lastFireTime + fireRate && canShoot)
        {
            ShootAtEnemy();
        }
        
    }

    // Função para encontrar o inimigo mais próximo
    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float curDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (curDistance < distance)
            {
                nearest = enemy;
                distance = curDistance;
            }
        }

        if (distance <= range)
        {
            GetComponent<Animator>().SetBool("Shoot", true);
            return nearest;
        }
        else
        {
            GetComponent<Animator>().SetBool("Shoot", false);
            return null;
        }
    }

    // Função para rotacionar a torre para o inimigo mais próximo
    private void RotateTowardsEnemy()
    {
        Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion lookRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10.0f);
    }

    // Função para atirar no inimigo mais próximo
    private void ShootAtEnemy()
    {
        GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        newProjectile.GetComponent<Rigidbody2D>().velocity = (nearestEnemy.transform.position - transform.position).normalized * 10.0f;
        Destroy(newProjectile, 5f);
        lastFireTime = Time.time;
    }

    public void SetCanShoot()
    {
        canShoot = true;
    }

    public void DisableCanShoot()
    {
        canShoot = false;
    }

}
