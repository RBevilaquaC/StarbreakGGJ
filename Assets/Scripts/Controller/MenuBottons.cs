using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBottons : MonoBehaviour
{
    public static Transform buttons;

    private void Awake()
    {
        buttons = transform;
    }
}
