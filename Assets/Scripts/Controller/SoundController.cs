using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class SoundController : MonoBehaviour
{
    private EventInstance dayMusic;
    private EventInstance nightMusic;
    private EventInstance ambientMusic;
    

    private void PlayMorningSong()
    {
        Debug.Log("Dia Começou");
        nightMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        dayMusic.start();
        //RuntimeManager.PlayOneShot("event:/Music/DuringDaySong");
    }
    // Start is called before the first frame update
    void Start()
    {
        dayMusic = RuntimeManager.CreateInstance("event:/Music/DuringDaySong");
        nightMusic = RuntimeManager.CreateInstance("event:/Music/DuringNightSong");
        ambientMusic = RuntimeManager.CreateInstance("event:/Music/AmbientMusic");
        //ambientMusic.setParameterByName("ParameterName", isDay);
        DayController.dayComes += PlayMorningSong;
        DayController.nightArrive += PlayNightSong;
    }

    private void PlayNightSong()
    {
        dayMusic.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        Debug.Log("A noite começou");
        //var nightMusic = RuntimeManager.CreateInstance("event:/Music/DuringNightSong");
        nightMusic.start();
        //RuntimeManager.PlayOneShot("event:/Music/DuringNightSong");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
