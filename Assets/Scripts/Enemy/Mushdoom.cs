using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Mushdoom : MonoBehaviour
{
    [SerializeField] private int mushdoomDamage;
    [SerializeField] private int poisonDuration;
    [SerializeField] private bool inTheFog;
    private PlayerLife player;
    private bool damageAgain;



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
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerLife>().TakeDamage(mushdoomDamage);
            Debug.Log("damage1");
            other.GetComponent<PlayerLife>().ApplyPoison(mushdoomDamage, poisonDuration);
            
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        
        //yield return new WaitForSecondsRealtime(2f);
        //Debug.Log("Damage");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
        }

    }
}
