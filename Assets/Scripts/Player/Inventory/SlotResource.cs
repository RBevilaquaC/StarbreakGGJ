using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotResource : MonoBehaviour
{
    #region Parameters

    public bool haveResource;
    public bool fullStack;
    public int type;
    public int CurrentResource;

    private TMP_Text textAmount;
    private Image imageIcon;

    #endregion

    private void Start()
    {
        textAmount = transform.GetChild(0).GetComponent<TMP_Text>();
        imageIcon = transform.GetChild(1).GetComponent<Image>();
        type = -1;
        UpdateUI();
    }

    public void SlotReset()
    {
        haveResource = false;
        fullStack = false;
        CurrentResource = 0;
        type = -1;
        UpdateUI();
    }

    public void UpdateUI()
    {
        imageIcon.enabled = true;
        textAmount.text = CurrentResource.ToString();
        if (!haveResource)
        {
            imageIcon.enabled = false;
            textAmount.text = "";
        }
    }

    public void UpdateIcon(Sprite newSprite)
    {
        imageIcon.sprite = newSprite;
    }
}
