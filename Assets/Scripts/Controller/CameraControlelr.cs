using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlelr : MonoBehaviour
{
    #region Parameters

    [Range(0.0F, 1.0F)]
    [SerializeField] private float smoothIntensity;
    [SerializeField] private Vector3 offSet;
    private Transform playerPos;

    #endregion

    private void Start()
    {
        playerPos = PlayerStatus.playerObj.transform;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position + offSet, smoothIntensity);
    }
}
