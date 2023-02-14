using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    #region Parameters

    public static GameObject playerObj;
    public static PlayerStatus status;
    private Animator anim;
    [HideInInspector] public bool isAttacking;
    
    [Header("Moviment Settings")] 
    [SerializeField] private float movimentSpeed;
    [SerializeField] private float rotateModifier;
    
    #endregion

    private void Awake()
    {
        playerObj = gameObject;
        status = this;
    }

    private void Start()
    {
        if(GameController.gm != null)
            if(GameController.gm.isContinue) LoadGame();
    }

    public float GetMovimentSpeed()
    {
        return movimentSpeed;
    }

    public float GetRotateModifier()
    {
        return rotateModifier;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
        
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();
        
        ResourceManage.resourceManage.SetInventoryList(data.playerResourceList);
        Container.container.containerInventoryList = data.containerResourceList;
        DayController.dayController.dayCount = data.dayCount;
        GetComponent<PlayerLife>().SetCurrentLife(data.playerCurrentLife);
        
        ResourceManage.resourceManage.SortInventory();
        Container.container.SortContainer();
    }
}
