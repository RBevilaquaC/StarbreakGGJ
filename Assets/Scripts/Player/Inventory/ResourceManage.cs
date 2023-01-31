using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ResourceManage : MonoBehaviour
{
    #region Parameters

    //typeRespurce: 1==Wood, 2==Stone;

    private SlotResource[] slotsResource;
    private int maxResource = 30;

    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite stoneSprite;

    #endregion

    private void Start()
    {
        slotsResource = new SlotResource[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            slotsResource[i] = transform.GetChild(i).GetComponent<SlotResource>();

    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) AddResource(1,20);
    }

    public void AddResource(int type, int AmountResource)
    {
        bool canStorage = false;
        for (int i = 0; i < slotsResource.Length; i++)
        {
            if (slotsResource[i].type == type && !slotsResource[i].fullStack)
            {
                canStorage = true;
                slotsResource[i].CurrentResource += AmountResource;
                slotsResource[i].UpdateUI();
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].UpdateUI();
                    print(remainResource);
                    //AddResource(type,remainResource);
                }
                break;
            }
            else if (!slotsResource[i].haveResource)
            {
                canStorage = true;
                slotsResource[i].haveResource = true;
                slotsResource[i].type = type;
                slotsResource[i].CurrentResource += AmountResource;
                slotsResource[i].UpdateUI();
                switch (type)
                {
                    case 1:
                        slotsResource[i].UpdateIcon(woodSprite);
                        break;
                    case 2:
                        slotsResource[i].UpdateIcon(stoneSprite);
                        break;
                }
                if (slotsResource[i].CurrentResource > maxResource)
                {
                    int remainResource = slotsResource[i].CurrentResource - maxResource;
                    slotsResource[i].CurrentResource = maxResource;
                    slotsResource[i].UpdateUI();
                    print(remainResource);
                    //AddResource(type,remainResource);
                }
                break;
            }
        }
        
    }
}
