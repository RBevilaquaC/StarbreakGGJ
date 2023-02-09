using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Dart : MonoBehaviour
{
   public GameObject owner;
   private void OnTriggerEnter2D(Collider2D col)
   {
      if(col.gameObject != owner)
      {
         if (col.GetComponent<EnemyLife>() != null)
         {
            col.GetComponent<EnemyLife>().TakeDamage(4);
            gameObject.SetActive(false);
         }

         if (col.GetComponent<PlayerLife>() == null)
         {
            gameObject.SetActive(false);
         }
      }
   }
}
