using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Transform ClockHand;
    private static float Day { get; set; }
    public static int Hour { get; private set;  }
    public static float Seconds { get; private set;  }
    private float minuteToRealTime = 0.5f;
    private float timer;
    [SerializeField] private float morningDuration;
    [SerializeField] private float dayDuration;
    private bool isDay;
    [SerializeField]private Volume nightVolume;  

// Start is called before the first frame update
    void Start()
    {
        Seconds = 0;
        Day = 0;
        nightVolume.weight = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        Seconds += Time.deltaTime;
        if (Seconds >= dayDuration)
        {
            nightVolume.weight = 0f;
            Day++;
            Seconds = 0;
            Debug.Log(Day.ToString());
        }

        if (morningDuration <= Seconds)
        {
            nightVolume.weight = 0.5f;
        }

        //float secondsTime = Mathf.FloorToInt(Seconds);
        ClockHand.eulerAngles = Vector3.back * (Seconds * 360 / dayDuration);
    }
}
