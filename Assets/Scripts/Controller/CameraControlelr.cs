using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControlelr : MonoBehaviour
{
    #region Parameters

    [Range(0.0F, 1.0F)]
    [SerializeField] private float smoothIntensity;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private Vector3 limitSW;
    [SerializeField] private Vector3 limitNE;
    private Transform playerPos;

    #endregion

    private void Start()
    {
        playerPos = PlayerStatus.playerObj.transform;
        transform.position = playerPos.position;
    }

    private void LateUpdate()
    {
        if (playerPos.position.x > limitSW.x && playerPos.position.y > limitSW.y &&
            playerPos.position.x < limitNE.x && playerPos.position.y < limitNE.y)
            FollowPlayer();
        else if (playerPos.position.x > limitSW.x && playerPos.position.x < limitNE.x) FollowPlayerXaxis();
        else if (playerPos.position.y > limitSW.y && playerPos.position.y < limitNE.y) FollowPlayerYaxis();

    }
    
    private void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, playerPos.position + offSet, smoothIntensity);
    }

    private void FollowPlayerXaxis()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.position.x + offSet.x,transform.position.y + offSet.y,playerPos.position.z + offSet.z), smoothIntensity);
    }
    private void FollowPlayerYaxis()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3( transform.position.x + offSet.x,playerPos.position.y +offSet.y,playerPos.position.z + offSet.z), smoothIntensity);
    }
}
