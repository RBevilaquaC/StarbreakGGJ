using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : LifeSystem
{
    #region Parameters

    [SerializeField] private Slider lifeBar;
    [SerializeField] private GameObject gameOverPanel;
    public bool isPoisoned;
    private float poisonDuration;
    private int poisonDamage;

    #endregion

    protected override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        UpdateLifeBar();
    }

    protected override void Death()
    {
        base.Death();
        gameOverPanel.SetActive(true);
    }
    

    public override void TakeDamage(int damageAmount)
    {
        currentLife -= damageAmount;
        if (currentLife <= 0)
        {
            currentLife = 0;
            UpdateLifeBar();
            Death();
        }
        UpdateLifeBar();
    }

    private void UpdateLifeBar()
    {
        lifeBar.value = (float)currentLife / maxLife;
    }

    private void Update()
    {
    }

    public void ApplyPoison(int damage, float duration)
    {
        StartCoroutine(Poisoning(duration, damage));
    }

    private IEnumerator Poisoning(float duration, float damage)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            TakeDamage((int)(duration * Time.deltaTime));
            elapsedTime += Time.deltaTime;
            Debug.Log("dMGE2");
            yield return new WaitForSeconds(1f);
        }
    }
}
