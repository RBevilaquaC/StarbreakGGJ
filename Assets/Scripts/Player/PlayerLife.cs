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
}
