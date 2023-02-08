using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

public class DayController : MonoBehaviour
{
    #region Parameters

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [Range(30,600)]
    [SerializeField]private float dayDuration;
    private float currentTime;
    public static DayController dayController;
    private Light2D light;
    public int dayCount = 1;
    public bool isDay;

    #endregion

    private void Awake()
    {
        dayController = this;
    }

    private void Start()
    {
        light = GetComponent<Light2D>();
        light.intensity = 0;
        isDay = true;
        transform.position = startPos;
        GetComponent<Rigidbody2D>().velocity = Vector2.right * 200/dayDuration;
    }

    private void Update()
    {
        if(isDay){
            if(currentTime > 10f) 
                PlayerStatus.playerObj.transform.GetChild(1).gameObject.SetActive(false);
            if (currentTime < 10f)
            {
                light.intensity += Time.deltaTime / 10;
            }
            else if (currentTime > dayDuration - 10f)
            {
                PlayerStatus.playerObj.transform.GetChild(1).gameObject.SetActive(true);
                light.intensity -= Time.deltaTime / 10;
            }
        }
        if (currentTime >= dayDuration) ChangeState();
        else currentTime += Time.deltaTime;
    }

    private void ChangeState()
    {
        if (isDay)
        {
            isDay = false;
            light.intensity = 0;
            currentTime = 0;
            transform.position = startPos;
            PlayerStatus.playerObj.transform.GetChild(1).gameObject.SetActive(true);
            SpawnManager.spawnManager.SpawnEnemies();
        }
        else
        {
            isDay = true;
            light.intensity = 0;
            currentTime = 0;
            dayCount++;
            transform.position = startPos;
        }
    }
}
