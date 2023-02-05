using System.Collections;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLife : LifeSystem
{
    private DropSystem dropSystem;

    protected override void Start()
    {
        base.Start();
        dropSystem = GetComponent<DropSystem>();
    }

    protected override void Death()
    {
        base.Death();
        dropSystem.DropItem(Random.Range(0,3),transform.position);
        ResourceManage.resourceManage.GetResourceVarietyAmount();
    }
}
