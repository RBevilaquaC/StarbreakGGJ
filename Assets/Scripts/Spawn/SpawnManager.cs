using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    #region Parameters

    public static SpawnManager spawnManager;
    
    [Header("Spawnable Setting")]
    [SerializeField] private GameObject[] spawnableObjs;
    [SerializeField] private float[] enemiesSpawnChance;
    [SerializeField] private int[] sizeEachPool;
    [SerializeField] private float[] dayMultiplayer;

    private int currentResourceIndex = 0;

    #endregion

    private void Awake()
    {
        spawnManager = this;
    }

    private void Start()
    {
        HandleInput();
        FillPools();
    }

    public void SpawnEnemies()
    {
        for(int i = 1; i < spawnableObjs.Length; i++)
        for (int j = 0;
             j < DayController.dayController.dayCount * dayMultiplayer[i] && j < transform.GetChild(i).childCount;
             j++)
        {
            GameObject enemy = transform.GetChild(i).GetChild(j).gameObject;
            enemy.SetActive(true);
            enemy.GetComponent<EnemyLife>().Respawn();
        }
    }
    
    private void FillPools()
    {
        for (int j = 0; j < spawnableObjs.Length ; j++)
        {
            GameObject parent = new GameObject();
            parent.transform.parent = gameObject.transform;
            parent.name = spawnableObjs[j].name + "Pool";
            for(int i = 0; i < sizeEachPool[j]; i++)
            {
                GameObject newEnemy = Instantiate(spawnableObjs[j], parent.transform);
                newEnemy.SetActive(false);
            }
        }
    }

    public GameObject DeliveryResource()
    {
        currentResourceIndex++;
        if(currentResourceIndex >= transform.GetChild(0).childCount)
        {
            currentResourceIndex = 0;
            return transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).gameObject;
        }
        return transform.GetChild(0).GetChild(currentResourceIndex - 1).gameObject;
    }

    private void HandleInput()
    {
        if (enemiesSpawnChance.Length < spawnableObjs.Length)
        {
            float[] aux = new float[spawnableObjs.Length];
            for (int i = 0; i < enemiesSpawnChance.Length; i++) aux[i] = enemiesSpawnChance[i];
            enemiesSpawnChance = new float[spawnableObjs.Length];
            for (int i = 0; i < enemiesSpawnChance.Length; i++) enemiesSpawnChance[i] = aux[i];
        }
        if (sizeEachPool.Length < spawnableObjs.Length)
        {
            int[] aux = new int[spawnableObjs.Length];
            for (int i = 0; i < aux.Length; i++) aux[i] = 1;
            for (int i = 0; i < sizeEachPool.Length; i++) aux[i] = sizeEachPool[i];
            sizeEachPool = new int[spawnableObjs.Length];
            for (int i = 0; i < sizeEachPool.Length; i++) sizeEachPool[i] = aux[i];
        }
    }
}
