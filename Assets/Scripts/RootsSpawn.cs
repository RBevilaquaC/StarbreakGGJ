using System;
using UnityEngine;
using System.Collections;

public class RootsSpawn : MonoBehaviour {

    private ParticleSystem particles;
    [SerializeField] private float growingTime; 

    private void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
        DayController.dayComes += DestroyRootsDuringDay;
        DayController.nightArrive +=  GrowRoots;
    }

    // Update is called once per frame
    void Update () {
        //After 4 seconds, pause particles
        /*if(Time.timeSinceLevelLoad > particles.duration) {
            particles.Pause();
        }*/
    }

    private void DestroyRootsDuringDay()
    {
        var particlesSizeOverLifetime = particles.sizeOverLifetime;
        particlesSizeOverLifetime.enabled = true;
        //var particlesMain = particles.main;
        //particlesMain.simulationSpeed = 0;
        particles.Play();


    }

    private void GrowRoots()
    {
        particles.Clear();
        var particlesSizeOverLifetime = particles.sizeOverLifetime;
        particlesSizeOverLifetime.enabled = false;
        particles.Play();
        var particlesMain = particles.main;
        particlesMain.startSpeed = 5;
        particlesMain.simulationSpeed = 1;
        StartCoroutine(GrowTime());

    }

    IEnumerator GrowTime()
    {
        yield return new WaitForSeconds(4f);
        particles.Pause();
    }
}