using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVCAVolume : MonoBehaviour
{
    private FMOD.Studio.VCA VCAController;

    private Slider slider;

    public string VCAName;
    
    // Start is called before the first frame update
    void Start()
    {
        VCAController = FMODUnity.RuntimeManager.GetVCA("vca:/" + VCAName);
        slider = GetComponent<Slider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        VCAController.setVolume(volume);
    }
}
