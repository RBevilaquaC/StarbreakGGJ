using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    #region Parameters

    private Rigidbody2D rb;
    private Vector2 dirToMove;
    private Animator anim;
    private Transform spritePos;

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spritePos = transform.GetChild(0);
        anim = spritePos.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        dirToMove.x = Input.GetAxis("Horizontal");
        dirToMove.y = Input.GetAxis("Vertical"); 
    }

    private void FixedUpdate()
    {
        Move();
        UpdateRotation();   
    }

    private void Move()
    {
        if(dirToMove != Vector2.zero && !PlayerStatus.status.isAttacking)
        {
            rb.velocity = dirToMove.normalized * PlayerStatus.status.GetMovimentSpeed();
            anim.Play("Walking");
        } else if(!PlayerStatus.status.isAttacking)
        {
            rb.velocity = Vector2.zero;
            anim.Play("Idle");
        }
    }

    private void UpdateRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - spritePos.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        Vector3 targetRotation = new Vector3(0, 0, angle-90);
        spritePos.rotation = (Quaternion.Lerp(spritePos.rotation, Quaternion.Euler(targetRotation), PlayerStatus.status.GetRotateModifier()));
    }
    
}
