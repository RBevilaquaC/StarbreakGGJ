using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{   

    [SerializeField] private GameObject enemyPrefab;
    private List<GameObject> enemies = new List<GameObject>();
    private int limitActiveEnemies;
    private float timeRespawn;
    private IEnumerator coroutineSpawnEnemy = null;


    private void OnEnable() {
        foreach (var enemy in enemies)
            {   
                enemy.SetActive(true);  
            }
        coroutineSpawnEnemy = SpawnEnemy(timeRespawn);

        StartCoroutine(coroutineSpawnEnemy);
    }

    private void OnDisable() {
        foreach (var enemy in enemies)
            {   
                enemy.SetActive(false);  
            }      
        StopCoroutine(coroutineSpawnEnemy);
    }

    public void SetTimeRespawn(float seconds){
        StopCoroutine(coroutineSpawnEnemy);
        timeRespawn = seconds;
        coroutineSpawnEnemy = SpawnEnemy(timeRespawn);
        
        if(gameObject.activeSelf){
            StartCoroutine(coroutineSpawnEnemy);
        }

    }

    IEnumerator SpawnEnemy(float seconds){
        while(enemies.Count != 0){
            foreach (var enemy in enemies)
            {   
                yield return new WaitForSeconds(seconds);
                enemy.SetActive(true);  
            }
        }
    }

   
    public void AddEnemyToList(int n){
        if(coroutineSpawnEnemy != null){
            StopCoroutine(coroutineSpawnEnemy);
        }
        
        for (int i = 0; i < n; i++)
        {
            GameObject oneEnemy = Instantiate(enemyPrefab, 
                    gameObject.transform.position, 
                    Quaternion.identity);
            
            enemies.Add((GameObject)oneEnemy);
        }
        
        if(gameObject.activeSelf && coroutineSpawnEnemy != null){
            StartCoroutine(coroutineSpawnEnemy);
        }
    }
}
