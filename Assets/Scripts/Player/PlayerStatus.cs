using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    #region Parameters

    public static GameObject playerObj;
    public static PlayerStatus status;
    
    [Header("Moviment Settings")] 
    [SerializeField] private float movimentSpeed;
    
    [Header("FieldView Settings")] 
    [Range(1f,360f)]
    [SerializeField] private float fov;
    [SerializeField] private int rayCount;
    [SerializeField] private float viewDistance;
    [SerializeField] private LayerMask layerMaskView;
    [SerializeField] private float rotateModifier;

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
    
    public float GetFov()
    {
        return fov;
    }

    public int GetRayCount()
    {
        return rayCount;
    }

    public float GetViewDistance()
    {
        return viewDistance;
    }

    public LayerMask GetLayerMaskView()
    {
        return layerMaskView;
    }

    public float GetRotateModifier()
    {
        return rotateModifier;
    }
}
