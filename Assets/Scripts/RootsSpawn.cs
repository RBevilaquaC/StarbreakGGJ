using System;
using UnityEngine;
using System.Collections;

public class RootsSpawn : MonoBehaviour {

    private ParticleSystem particles;

    private void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update () {
        //After 4 seconds, pause particles
        if(Time.timeSinceLevelLoad > particles.duration) {
            particles.Pause();
        }
    }
}