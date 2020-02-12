using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellableObject : MonoBehaviour
{
    public float ObjectCost;
   
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Seller>().recieve >= ObjectCost)
        {
            GameObject.Find("Player").GetComponent<Player>().Gold += ObjectCost;
            Destroy(gameObject);
        }
    }
}
