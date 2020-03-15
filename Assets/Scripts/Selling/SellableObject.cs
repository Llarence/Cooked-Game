using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellableObject : MonoBehaviour
{
    public bool store;
    public bool banana;
    public bool carrot;
    public bool fish_001;
    public bool fish_002;
    public bool grape;
    public bool apple;
    public bool fruitSalad;
    public bool pear;
    public bool garlic;
    public bool corn;
    public bool bread;

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
            if (fruitSalad == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[6]++;
                Destroy(gameObject);
            }
            if (pear == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[7]++;
                Destroy(gameObject);
            }
            if (garlic == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[8]++;
                Destroy(gameObject);
            }
            if (corn == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[9]++;
                Destroy(gameObject);
            }
            if (bread == true)
            {
                GameObject.Find("Manager").GetComponent<Inventory>().FoodCount[10]++;
                Destroy(gameObject);
            }
        }
    }
}
