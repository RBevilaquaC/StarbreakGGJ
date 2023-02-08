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

    private void Awake()
    {
        imageIcon = GetComponent<SpriteRenderer>();
        textAmount = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (type >= ResourceManage.resourceManage.GetResourceVarietyAmount()) type = 0;
        
        imageIcon.sprite = ResourceManage.resourceManage.GetResourceSprite(type);
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
    
    public void SetValues(int newType, int newAmount)
    {
        type = newType;
        amount = newAmount;
        imageIcon.sprite = ResourceManage.resourceManage.GetResourceSprite(type);
        UpdateUI();
    }
}
