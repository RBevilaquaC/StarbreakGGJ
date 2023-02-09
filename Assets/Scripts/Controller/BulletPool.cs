using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    #region Parameters

    public static BulletPool bulletPool;
    [SerializeField] private int poolSize;
    [SerializeField] private GameObject bulletDefault;
    private int currentBullet;

    #endregion

    private void Awake()
    {
        bulletPool = this;
        fillPool(poolSize);
    }

    private void fillPool(int bulletAmount)
    {
        for (;bulletAmount>0;bulletAmount--)
        {
            GameObject newProjectPrefab = Instantiate(bulletDefault, gameObject.transform);
            newProjectPrefab.SetActive(false);
        }
    }

    public GameObject DeliveryBullet()
    {
        GameObject currentBulletToDelivery = transform.GetChild(currentBullet).gameObject;
        currentBullet++;
        currentBullet %= poolSize;
        return currentBulletToDelivery;
    }
}
