using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    #region Parameters

    
    
    [SerializeField] public int id;
    public int[] resourceList;
    
    private SlotResource[] slotsResource;
    
    #endregion

    private void Start()
    {
        resourceList = new int[ResourceManage.resourceManage.GetResourceVarietyAmount()];
    }
}
