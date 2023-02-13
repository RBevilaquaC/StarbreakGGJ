using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

public class DayController : MonoBehaviour
{
    #region Parameters

    [SerializeField] private Vector3 startPos;
    [SerializeField] public Vector3 endPos;
    [Range(30,600)]
    [SerializeField]private float dayDuration;
    private float currentTime;
    public static DayController dayController;
    private Light2D light;
    public int dayCount = 1;
    public bool isDay;
    public delegate void DayEvent();

    public delegate void NightEvent();

    public static event DayEvent dayComes;
    public static event NightEvent nightArrive;

    #endregion

    private void Awake()
    {
        dayController = this;
    }

    private void Start()
    {

        RuntimeManager.StudioSystem.setParameterByName("MenuTransition", 1);
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
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isDay", 1);
            nightArrive?.Invoke();
            isDay = false;
            light.intensity = 0;
            currentTime = 0;
            transform.position = startPos;
            PlayerStatus.playerObj.transform.GetChild(1).gameObject.SetActive(true);
            SpawnManager.spawnManager.SpawnEnemies();
        }
        else
        {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("isDay", 0);
            dayComes?.Invoke();
            isDay = true;
            light.intensity = 0;
            currentTime = 0;
            dayCount++;
            transform.position = startPos;
        }
    }
}