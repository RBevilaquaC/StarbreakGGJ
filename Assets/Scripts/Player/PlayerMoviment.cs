using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    #region Parameters

    private Rigidbody2D rb;
    private Vector2 dirToMove;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        dirToMove.x = Input.GetAxis("Horizontal");
        dirToMove.y = Input.GetAxis("Vertical");    
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = dirToMove.normalized * PlayerStatus.status.GetMovimentSpeed();
    }
}
