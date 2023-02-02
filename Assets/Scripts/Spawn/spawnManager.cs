using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] private bool active = true;
    public static int limitActiveEnemies= 3;
    public static int timeRespawn= 2;
   
    private GameObject[] respawns;

    private GameObject sun;
    private bool hasSun; 



    void Start() {
        sun = GameObject.Find("Sun");
        respawns = GameObject.FindGameObjectsWithTag("Respawn");
    }

    void Update(){
        //Associa os respawn a noite, se o sol estiver desativo
        //os respawn ficam ativos: 
        
        // hasSun = sun.activeSelf;
        // active = !hasSun;
        
       
        if(!active){
            foreach (var respawn in respawns)
            {
                respawn.SetActive(false);
            }
        }else{
            foreach (var respawn in respawns)
            {
                respawn.SetActive(true);
            }
        }

        
   }


}
