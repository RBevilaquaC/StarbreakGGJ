using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    #region Parameters

    private SlotResource[] slotsResource;
    public int[] containerInventoryList;
    private int maxStackResource;
    public bool isOpen;

    public static Container container;
    
    #endregion

    private void Awake()
    {
        container = this;
    }

    private void Start()
    {
        isOpen = transform.parent.gameObject.activeSelf;
        maxStackResource = ResourceManage.resourceManage.GetMaxStackResource();
        containerInventoryList = new int[ResourceManage.resourceManage.GetResourceVarietyAmount()];
        FillSlotResource();
        CloseContainer();
    }

    
    private void Update()
    {
        //if(Input.GetButtonDown("Fire1")) AddResource(0,20);
        //if(Input.GetButtonDown("Fire2")) AddResource(1,20);
    }
    
    private void FillSlotResource()
    {
        slotsResource = new SlotResource[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            slotsResource[i] = transform.GetChild(i).GetComponent<SlotResource>();
    }
    
    public void AddResource(int type, int amountResource)
    {
        if(amountResource == 0) return;
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                slotsResource[i].CurrentResource += amountResource;
                containerInventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxStackResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxStackResource;
                    containerInventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxStackResource;
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
                containerInventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(ResourceManage.resourceManage.GetResourceSprite(type));
                if (slotsResource[i].CurrentResource > maxStackResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxStackResource;
                    containerInventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxStackResource;
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
                containerInventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxStackResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxStackResource;
                    containerInventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxStackResource;
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
                containerInventoryList[type] += amountResource;
                slotsResource[i].UpdateUI();
                slotsResource[i].UpdateIcon(ResourceManage.resourceManage.GetResourceSprite(type));
                if (slotsResource[i].CurrentResource > maxStackResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxStackResource;
                    containerInventoryList[type] -= remainResource;
                    slotsResource[i].CurrentResource = maxStackResource;
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

    public void TransferResource(SlotResource slot)
    {
        //SlotResource slotResource = GetComponent<SlotResource>();
        int slotResource = slot.CurrentResource;
        int remainResource = ResourceManage.resourceManage.ReceiveTransferResource(slot.type,slot.CurrentResource);
        containerInventoryList[slot.type] -= (slotResource - remainResource) ;
        if (remainResource > 0) slot.CurrentResource = remainResource;
        else slot.SlotReset();
    }

    public void CloseContainer()
    {
        isOpen = false;
        transform.parent.gameObject.SetActive(isOpen);
    }
    public void OpenContainer()
    {
        isOpen = true;
        transform.parent.gameObject.SetActive(isOpen);
    }

    private void OnEnable()
    {
        isOpen = true;
    }
    
    private void OnDisable()
    {
        isOpen = false;
    }
    
    public void SortContainer()
    {
        foreach (var slot in slotsResource) slot.SlotReset();
        for(int i = 0; i < containerInventoryList.Length; i++) AddResource(i,containerInventoryList[i]);
        for(int i = 0; i < containerInventoryList.Length; i++) containerInventoryList[i] /= 2;
    }
}
