using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnScript : MonoBehaviour
{   
    [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> enemies;
    private int limitActiveEnemies;
    private int timeRespawn;

    void Start()
    {   
        limitActiveEnemies = spawnManager.limitActiveEnemies;
        timeRespawn = spawnManager.timeRespawn;

        enemies = new List<GameObject>();

        AddEnemyToList(limitActiveEnemies);
        
    }


    void Update()
    {
        
        StartCoroutine(RespawnEnemy(timeRespawn));
    
    }

    void AddEnemyToList(int n){
        for (int i = 0; i < n; i++)
        {
            GameObject oneEnemy = Instantiate(enemyPrefab, 
                    gameObject.transform.position, 
                    Quaternion.identity);
            
            oneEnemy.SetActive(false);
            enemies.Add((GameObject)oneEnemy);
        }
    }

    IEnumerator RespawnEnemy(int seconds){
        foreach (var enemy in enemies)
        {   
            enemy.SetActive(true);  
            yield return new WaitForSeconds(seconds);
        }
    }
}
