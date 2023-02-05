using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayController : MonoBehaviour
{
    #region Parameters

    private float dayDuration;
    private float currentTime;
    public static DayController dayController;

    #endregion

    private void Awake()
    {
        dayController = this;
    }

    private void Update()
    {
        
    }
}
