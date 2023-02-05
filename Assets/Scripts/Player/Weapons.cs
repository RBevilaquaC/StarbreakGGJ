using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{

    [SerializeField] private GameObject crossbow;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateCrossbow();
        }
    }

    private void InstantiateCrossbow()
    {
        if (ResourceManage.resourceManage.ResourceDelivery(0, 50) != 0
            && ResourceManage.resourceManage.ResourceDelivery(1, 10) != 0)
        {
            Instantiate(crossbow, gameObject.transform.position, gameObject.transform.rotation);
        }
        else Debug.Log("Sem recursos!");
    }

}
