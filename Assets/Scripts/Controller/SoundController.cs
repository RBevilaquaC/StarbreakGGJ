using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;

public class SoundController : MonoBehaviour
{
    private EventInstance dayMusic;
    private EventInstance nightMusic;
    

    private void PlayMorningSong()
    {
        dayMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        dayMusic.start();
        //RuntimeManager.PlayOneShot("event:/Music/DuringDaySong");
    }
    // Start is called before the first frame update
    void Start()
    {
        dayMusic = RuntimeManager.CreateInstance("event:/Music/DuringDaySong");
        nightMusic = RuntimeManager.CreateInstance("event:/Music/DuringNightSong");
        DayController.dayComes += PlayMorningSong;
        DayController.nightArrive += PlayNightSong;
    }

    private void PlayNightSong()
    {
        nightMusic.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //var nightMusic = RuntimeManager.CreateInstance("event:/Music/DuringNightSong");
        nightMusic.start();
        //RuntimeManager.PlayOneShot("event:/Music/DuringNightSong");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
