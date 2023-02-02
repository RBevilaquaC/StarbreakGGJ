using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ResourceManage : MonoBehaviour
{
    #region Parameters

    //typeRespurce: 0==Wood, 1==Stone;

    private SlotResource[] slotsResource;
    private int maxResource = 30;

    [SerializeField] private Sprite[] resourceSprite;
    [SerializeField] private Canvas canvas;

    public static ResourceManage resourceManage;
    private bool inventoryIsOpen;

    #endregion

    private void Awake()
    {
        resourceManage = this;
    }

    private void Start()
    {
        if (canvas.sortingLayerName == "Default") inventoryIsOpen = false;
        else inventoryIsOpen = true;
        if(inventoryIsOpen) CloseInventory();
        
        slotsResource = new SlotResource[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            slotsResource[i] = transform.GetChild(i).GetComponent<SlotResource>();
    }

    private void Update()
    {
        //if(Input.GetButtonDown("Fire1")) AddResource(1,20);
        //if(Input.GetButtonDown("Fire2")) AddResource(2,20);
        if (Input.GetButtonDown("InventoryKey") && !inventoryIsOpen) OpenInventory();
        if (Input.GetButtonDown("InventoryKey") && inventoryIsOpen) CloseInventory();
    }

    public void AddResource(int type, int amountResource, Resource stackResource)
    {
        bool canStorage = false;
        stackResource.SetActiveObj(false);
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                canStorage = true;
                slotsResource[i].CurrentResource += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    AddResource(type,remainResource,stackResource);
                }
                break;
            }
            else if (!slotsResource[i].haveResource)
            {
                canStorage = true;
                slotsResource[i].haveResource = true;
                slotsResource[i].type = type;
                slotsResource[i].CurrentResource += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(resourceSprite[type]);
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    AddResource(type,remainResource,stackResource);
                }
                break;
            }
        }

        if (!canStorage)
        {
            stackResource.SetActiveObj(true);
            stackResource.amount = amountResource;
            stackResource.UpdateUI();
        }
    }

    public void CloseInventory()
    {
        canvas.sortingLayerName = "Default";
        inventoryIsOpen = false;
    }
    public void OpenInventory()
    {
        canvas.sortingLayerName = "UI";
        inventoryIsOpen = true;
    }

    public Sprite GetResourceSprite(int typeID)
    {
        return resourceSprite[typeID];
    }
    
    public int GetResourceVarietyAmount()
    {
        return resourceSprite.Length;
    }
    
}
