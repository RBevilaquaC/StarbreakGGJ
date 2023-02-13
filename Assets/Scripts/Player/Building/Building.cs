using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    #region Parameters

    [SerializeField] private GameObject crossbow;
    private ResourceManage resourceManage;
    

    #endregion

    private void Start()
    {
        resourceManage = ResourceManage.resourceManage;
    }

    private void Update()
    {
        if (CheckResource() && Input.GetButtonDown("Build")) SpawnCrossBow();
    }

    private void SpawnCrossBow()
    {
        resourceManage.ResourceDelivery(0, 20);
        resourceManage.ResourceDelivery(1, 10);
        GameObject newCrossbow = Instantiate(crossbow);
        newCrossbow.transform.position = transform.position + Vector3.up;
    }


    private bool CheckResource()
    {
        return resourceManage.GetInventoryList()[0] >= 20 && resourceManage.GetInventoryList()[0] >= 10;
    }
}
