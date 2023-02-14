using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net;


public class GameController : MonoBehaviour
{
    #region Parameters

    public static GameController gm;
    public bool isContinue;
    
    #endregion

    private void Start()
    {
        
        if (GameController.gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        string path = Application.persistentDataPath + "/saveRoots.star";
        GetContinueButton().SetActive(File.Exists(path));
        
        SceneManager.sceneLoaded += this.OnLoadCallback;
    }

    private void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Menu")
        {
            string path = Application.persistentDataPath + "/saveRoots.star";
            GetContinueButton().SetActive(File.Exists(path));
        }
    }

    public void SetIsContinue(bool state)
    {
        isContinue = state;
    }
    private GameObject GetContinueButton()
    {
        return MenuBottons.buttons.GetChild(0).gameObject;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
