using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Mushdoom : MonoBehaviour
{
     private int mushdoomDamage = 1;
     private float poisonDuration = 12;
     private float poisonInterval = 3;
    [SerializeField] private bool inTheFog;
    private PlayerLife player;
    private bool damageAgain;
    private bool isTouching;



    // Start is called before the first frame update
    void Start()
    {
        //player = PlayerStatus.playerObj.GetComponent<PlayerLife>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerLife>() != null)
        {
            col.GetComponent<PlayerLife>().bePoisoned(poisonDamage, poisonDuration);
            //InvokeRepeating();
            //Invoke("DamagePlayer", 2);
            //StartCoroutine(AwaitforDamage());
            //col.GetComponent<PlayerLife>().bePoisoned(poisonDamage, poisonDuration);
        }
    }*/

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        Debug.Log("Bateu");
        col.gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.gameObject.GetComponent<NavMeshAgent>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerLife pl = other.GetComponent<PlayerLife>();
            pl.TakeDamage(mushdoomDamage);
            mushdoomDamage = (int) mushdoomDamage;
            pl.ApplyPoison(mushdoomDamage, poisonDuration, poisonInterval, pl);
            //Debug.Log("damage1");
            //StartCoroutine(other.GetComponent<PlayerLife>());
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTouching) return;
            StartCoroutine(DamageContact(other.GetComponent<PlayerLife>()));
            /*if (PlayerLife.isPoisoned != false) return;
            mushdoomDamage = (int) mushdoomDamage;
            other.GetComponent<PlayerLife>().ApplyPoison(mushdoomDamage, poisonDuration, poisonInterval);*/

        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //inTheFog = false;
        }

    }

    private IEnumerator DamageContact(PlayerLife pl)
    {
        isTouching = true;
        pl.TakeDamage(mushdoomDamage);
        yield return new WaitForSeconds(2f);
        isTouching = false;

    }
}
