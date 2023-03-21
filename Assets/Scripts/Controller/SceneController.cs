using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetContinueState(bool state)
    {
        GameController.gm.isContinue = state;
    }
    
    void Start()
    {
        RuntimeManager.StudioSystem.setParameterByName("MenuTransition", 0);
    }

}
