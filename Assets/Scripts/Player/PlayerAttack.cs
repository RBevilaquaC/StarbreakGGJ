using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    #region Parameters

    [SerializeField] private int damageAttack;
    private Animator anim;

    #endregion

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PlayerStatus.status.isAttacking)
        {
            PlayerStatus.status.isAttacking = true;
            Attack();
        }
    }

    private void Attack()
    {
        anim.Play("Attack");
        Invoke("DisableAttack",0.3f);
    }

    private void DisableAttack()
    {
        PlayerStatus.status.isAttacking = false;
    }

    
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.GetComponent<LifeSystem>() != null && col.GetComponent<PlayerStatus>() == null && PlayerStatus.status.isAttacking)
        {
            col.GetComponent<LifeSystem>().TakeDamage(damageAttack);
        }
    }
}
