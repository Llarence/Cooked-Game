using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellableObject : MonoBehaviour
{
    public float ObjectCost;

    public bool store;

    public bool banana;
    public bool carrot;
    public bool fish_001;
    public bool fish_002;
    public bool grape;
    public bool apple;

    void Update()
    {
        if (store == true)
        {
            if (banana == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[0]++;
                Destroy(gameObject);
            }
            if (carrot == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[1]++;
                Destroy(gameObject);
            }
            if (fish_001 == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[2]++;
                Destroy(gameObject);
            }
            if (fish_002 == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[3]++;
                Destroy(gameObject);
            }
            if (grape == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[4]++;
                Destroy(gameObject);
            }
            if (apple == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[5]++;
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Seller>() != null)
        {
            if (collision.gameObject.GetComponent<Seller>().recieve >= ObjectCost)
            {
                GameObject.Find("Player").GetComponent<Player>().Gold += ObjectCost;
                Destroy(gameObject);
            }
        }
    }
}
