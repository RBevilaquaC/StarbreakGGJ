using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    #region Parameters

    public static GameObject playerObj;
    public static PlayerStatus status;
    
    [Header("Moviment Settings")] 
    [SerializeField] private float movimentSpeed;
    
    #endregion

    private void Awake()
    {
        playerObj = gameObject;
        status = this;
    }

    public float GetMovimentSpeed()
    {
        return movimentSpeed;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
