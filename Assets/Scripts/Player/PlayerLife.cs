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
    public static bool isPoisoned;
    private float poisonDuration = 12;
    private int poisonDamage = 1;
    private float poisonIntervalDamage =3;

    [Serializable]
    private struct Effects
    {
        public String EffectName;
        public GameObject ParticleEffect;
            
    }

    [SerializeField] private List<Effects> PlayerParticles;

    #endregion

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        UpdateLifeBar();
    }

    protected override void Death()
    {
        base.Death();
        ResourceManage.resourceManage.CloseInventory();
        ResourceManage.resourceManage.enabled = false;
        Container.container.CloseContainer();
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

    protected override void Start()
    {
        base.Start();
        //StartCoroutine(Poisoning(poisonDuration, poisonIntervalDamage, poisonDamage));
    }
    
    public void ApplyPoison(int damage, float duration, float interval, PlayerLife pl)
    {
        if (isPoisoned) return;
        StartCoroutine(Poisoning(duration, interval, damage, pl));
    }

    private static IEnumerator Poisoning(float poisonDuration, float poisonIntervalDamage, int PoisonDamage, PlayerLife pl)
    {
        isPoisoned = true;
        var t = poisonDuration;
        pl.PlayParticleEffect("PoisonEffect");
        while ( t > 0)
        {
            yield return new WaitForSeconds(poisonIntervalDamage);
            pl.TakeDamage(1);
            --t;
        }
        isPoisoned = false;
        


        //isPoisoned = true;

    }
    public void PlayParticleEffect(string name)
    {
        foreach (Effects particle  in PlayerParticles)
        {
            if (particle.EffectName == name)
            {
                var newObj = Instantiate(particle.ParticleEffect, transform.position, Quaternion.identity);
                newObj.transform.parent = transform;
                newObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
