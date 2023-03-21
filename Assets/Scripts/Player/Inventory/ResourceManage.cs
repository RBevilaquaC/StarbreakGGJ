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
    
    [SerializeField] private Sprite[] resourceSprite;
    [SerializeField] private Canvas canvas;
    [SerializeField] private int maxResource = 30;

    public static ResourceManage resourceManage;
    private bool inventoryIsOpen;
    private int[] inventoryList;

    #endregion

    private void Awake()
    {
        resourceManage = this;
    }

    private void Start()
    {
        if (canvas.sortingLayerName == "Default") inventoryIsOpen = false;
        else CloseInventory();
        
        inventoryList = new int[resourceSprite.Length];
        
        FillSlotResource();
    }

    private void Update()
    {
        //if(Input.GetButtonDown("Fire1")) AddResource(1,20);
        //if(Input.GetButtonDown("Fire2")) AddResource(2,20);
        if (Input.GetButtonDown("InventoryKey") && !inventoryIsOpen) OpenInventory();
        else if (Input.GetButtonDown("InventoryKey") && inventoryIsOpen) CloseInventory();
    }

    private void FillSlotResource()
    {
        slotsResource = new SlotResource[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            slotsResource[i] = transform.GetChild(i).GetComponent<SlotResource>();
        
    }
    
    public void AddResource(int type, int amountResource, Resource stackResource)
    {
        if(amountResource == 0) return;
        bool canStorage = false;
        stackResource.SetActiveObj(false);
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                canStorage = true;
                slotsResource[i].CurrentResource += amountResource;
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
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
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(resourceSprite[type]);
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
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
    
    public void AddResource(int type, int amountResource)
    {
        if(amountResource == 0) return;
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                slotsResource[i].CurrentResource += amountResource;
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    AddResource(type,remainResource);
                }
                break;
            }
            else if (!slotsResource[i].haveResource)
            {
                slotsResource[i].haveResource = true;
                slotsResource[i].type = type;
                slotsResource[i].CurrentResource += amountResource;
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(resourceSprite[type]);
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    AddResource(type,remainResource);
                }
                break;
            }
        }
    }

    public int ReceiveTransferResource(int type, int amountResource)
    {
        if(amountResource == 0) return 0;
        bool canStorage = false;
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                canStorage = true;
                slotsResource[i].CurrentResource += amountResource;
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    ReceiveTransferResource(type,remainResource);
                }
                break;
            }
            else if (!slotsResource[i].haveResource)
            {
                canStorage = true;
                slotsResource[i].haveResource = true;
                slotsResource[i].type = type;
                slotsResource[i].CurrentResource += amountResource;
                inventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(resourceSprite[type]);
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    inventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].fullStack = true;
                    slotsResource[i].UpdateUI();
                    ReceiveTransferResource(type,remainResource);
                }
                break;
            }
        }

        if (!canStorage)
            return amountResource;
        return 0;
    }

    public int ResourceDelivery(int type, int resourceAmount)
    {
        if (inventoryList[type] < resourceAmount) return 0;
        for (int i = slotsResource.Length - 1; i >= 0; i--)
        {
            if (slotsResource[i].type == type)
            {
                if (slotsResource[i].CurrentResource <= resourceAmount)
                {
                    inventoryList[type] -= slotsResource[i].CurrentResource;
                    resourceAmount -= slotsResource[i].CurrentResource;
                    slotsResource[i].SlotReset();
                    ResourceDelivery(type,resourceAmount);
                }
                else
                {
                    inventoryList[type] -= resourceAmount;
                    slotsResource[i].CurrentResource -= resourceAmount;
                    slotsResource[i].UpdateUI();
                }
                break;
            }
        }
        return 1;
    }

    public void TransferResource(SlotResource slot)
    {
        if(Container.container.isOpen)
        {
            int slotResource = slot.CurrentResource;
            int remainResource = Container.container.ReceiveTransferResource(slot.type, slot.CurrentResource);
            inventoryList[slot.type] -= (slotResource - remainResource);
            if (remainResource > 0) slot.CurrentResource = remainResource;
            else slot.SlotReset();
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

    public int GetMaxStackResource()
    {
        return maxResource;
    }

    public int[] GetInventoryList()
    {
        return inventoryList;
    }

    public void SetInventoryList(int[] newInventory)
    {
        inventoryList = newInventory;
    }
    
    public void SortInventory()
    {
        foreach (var slot in slotsResource) slot.SlotReset();
        for(int i = 0; i < inventoryList.Length; i++) AddResource(i,inventoryList[i]);
        for(int i = 0; i < inventoryList.Length; i++) inventoryList[i] /= 2;
    }
}
