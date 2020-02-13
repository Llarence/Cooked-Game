using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellableObject : MonoBehaviour
{
    public float ObjectCost;

    public bool store;

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
