using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Resource : MonoBehaviour
{
    #region Parameters

    public int type;
    public int amount;

    private TMP_Text textAmount;
    private SpriteRenderer imageIcon;
    
    #endregion

    private void Start()
    {
        if (type >= ResourceManage.resourceManage.GetResourceVarietyAmount()) type = 0;
        
        imageIcon = GetComponent<SpriteRenderer>();
        imageIcon.sprite = ResourceManage.resourceManage.GetResourceSprite(type);
        textAmount = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        imageIcon.enabled = true;
        textAmount.text = amount.ToString();
    }

    public void SetActiveObj(bool state)
    {
        gameObject.SetActive(state);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<PlayerStatus>() != null)
        {
            ResourceManage.resourceManage.AddResource(type, amount, this);
        }
    }
}
