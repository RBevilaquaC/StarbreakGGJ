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
        coroutineSpawnEnemy = SpawnEnemy(timeRespawn);
        StartCoroutine(coroutineSpawnEnemy);
    }

    private void Start(){
        coroutineSpawnEnemy = SpawnEnemy(timeRespawn);
        StartCoroutine(coroutineSpawnEnemy);
    }
    
    public void SetTimeRespawn(float seconds){
        if(coroutineSpawnEnemy != null){
            StopCoroutine(coroutineSpawnEnemy);
        }
        timeRespawn = seconds;
        coroutineSpawnEnemy = SpawnEnemy(timeRespawn);
        
        if(gameObject.activeSelf){
            StartCoroutine(coroutineSpawnEnemy);
        }
    }

    IEnumerator SpawnEnemy(float seconds){    
        if (seconds<0.1f){seconds = 0.1f;}
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
                    transform.position, 
                    Quaternion.identity);
                    
            oneEnemy.SetActive(false);
            enemies.Add((GameObject)oneEnemy);
        }
        
        if(gameObject.activeSelf && coroutineSpawnEnemy != null){
            StartCoroutine(coroutineSpawnEnemy);
        }
    }
}
