using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    #region Parameters
    
    [SerializeField] protected int maxLife;
    protected int currentLife;

    #endregion

    protected virtual void Start()
    {
        currentLife = maxLife;
    }

    protected virtual void Heal(int healAmount)
    {
        currentLife += healAmount;
        currentLife %= maxLife;
    }

    protected virtual void Death()
    {
        gameObject.SetActive(false);
    }
    public virtual void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        if (currentLife <= 0) Death();
    }
}
