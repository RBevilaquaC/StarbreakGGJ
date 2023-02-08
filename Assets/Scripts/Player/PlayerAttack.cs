using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;

public class PlayerAttack : MonoBehaviour
{
    #region Parameters

    [SerializeField] private int damageAttack;
    [SerializeField] private CircleCollider2D cc;
    private Animator anim;

    #endregion

    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        cc.enabled = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !PlayerStatus.status.isAttacking && !Container.container.isOpen)
        {
            PlayerStatus.status.isAttacking = true;
            Attack();
        }
    }

    private void Attack()
    {
        cc.enabled = true;
        anim.Play("Attack");
        RuntimeManager.PlayOneShot("event:/SFX/ScytheAttack");
        Invoke("DisableAttack",0.3f);
    }

    private void DisableAttack()
    {
        cc.enabled = false;
        PlayerStatus.status.isAttacking = false;
    }

    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<LifeSystem>() != null && col.GetComponent<PlayerStatus>() == null && PlayerStatus.status.isAttacking)
        {
            col.GetComponent<LifeSystem>().TakeDamage(damageAttack);
        }
    }
}
