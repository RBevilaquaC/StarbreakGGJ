using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
 
    [SerializeField] private bool active;
    [SerializeField] private int limitActiveEnemies;
    [SerializeField] private float timeRespawn = 2;   
    private SpawnScript[] spawnScripts;
    private GameObject[] respawns;
    private GameObject sun;
    private bool hasSun; 

    void Awake() {
        // Get Objects
        spawnScripts = FindObjectsOfType(typeof(SpawnScript)) as SpawnScript[];
        sun = GameObject.Find("Sun");
        respawns = GameObject.FindGameObjectsWithTag("Respawn");

        // Configuração inicial spawns
        foreach (var spawnScript in spawnScripts)
        {
            spawnScript.SetTimeRespawn(timeRespawn);
            spawnScript.AddEnemyToList(limitActiveEnemies);    
        }
        SetActiveRespawn(active);        
    }

    void Update(){
        //Associa os respawn a noite, se o sol estiver desativo
        //os respawn ficam ativos: 
        
        // hasSun = sun.activeSelf;
        // active = !hasSun;

        SetActiveRespawn(active);        
   }

    void SetActiveRespawn(bool b){
        foreach (var respawn in respawns)
            {
                respawn.SetActive(b);
            }
    }

    public void SetTimeRespawn(){
        foreach (var spawnScript in spawnScripts)
        {
            spawnScript.SetTimeRespawn(timeRespawn);
        }
    }

    public void AddEnemyToList(){
        foreach (var spawnScript in spawnScripts)
        {
            spawnScript.AddEnemyToList(limitActiveEnemies);
        }
    }

}
