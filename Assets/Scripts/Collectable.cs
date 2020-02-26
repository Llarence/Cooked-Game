using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string food;

    public bool selected;
    public bool active = true;
    public bool ground;
    public bool tree;

    private GameObject Manager;
    public GameObject Fruit;
    public GameObject Spawner;

    public float timer;
    public float fruitCount = 5;
    public float maxFruit = 5;

    public int respawnTime;

    // For carrot type "carrot" into the string
    // For grape type "grape" into the string

    void Update()
    {
        Manager = GameObject.Find("Manager");
        if (ground == true)
        {
            if (active == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;

                if (selected == true)
                {
                    Instantiate(Fruit, Spawner.transform.position, Spawner.transform.rotation);
                    active = false;
                    selected = false;
                }
            }
            else if (active == false)
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
        if (tree == true)
        {
            if (fruitCount != maxFruit && timer >= respawnTime)
            {
                fruitCount++;
                timer = 0;
            }
            else if (fruitCount != maxFruit)
            {
                timer += 1 * Time.deltaTime;
            }
            else if (fruitCount == maxFruit)
            {
                timer = 0;
            }
            if (selected == true && fruitCount != 0)
            {
                Instantiate(Fruit, Spawner.transform.position, Spawner.transform.rotation);
                selected = false;
                fruitCount--;
            }
            else
            {
                selected = false;
            }
        }
    }
}
