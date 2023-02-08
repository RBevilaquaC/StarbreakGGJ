using System.Collections;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : LifeSystem
{
    protected override void Death()
    {
        base.Death();
        DropResource();
        Invoke("Respawn",5f);
    }

    public void Respawn()
    {
        if (!DayController.dayController.isDay)
        {
            gameObject.SetActive(true);
            Vector3 playerPos = PlayerStatus.playerObj.transform.position;
            transform.position =  playerPos +
                                  (new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0).normalized)*40;

            currentLife = maxLife;
        }
    }


    private void DropResource()
    {
        int variety = ResourceManage.resourceManage.GetResourceVarietyAmount();
        GameObject drop = SpawnManager.spawnManager.DeliveryResource();
        drop.SetActive(true);
        drop.transform.position = transform.position;
        drop.GetComponent<Resource>().SetValues(Random.Range(0,variety),Random.Range(4,10));
    }
}
