using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string food;

    public bool selected;
    public bool active = true;

    public GameObject Manager;

    public float timer;
    public int respawnTime;

    // For carrot type "carrot" into the string
    // For grape type "grape" into the string

    void Update()
    {
        Manager = GameObject.Find("Manager");

        if (active == true)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            if (food == "carrot")
            {
                if (selected == true)
                {
                    Manager.GetComponent<Inventory>().FoodCount[1]++;
                    active = false;
                    selected = false;
                }
            }
        }else if (active == false)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            selected = false;
            timer += 1 * Time.deltaTime;
        }

        if (timer >= respawnTime)
        {
            active = true;
            timer = 0;
        }
    }
}
