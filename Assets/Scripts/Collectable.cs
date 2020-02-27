using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public bool selected;
    public bool active = true;
    public bool ground;
    public bool tree;

    public GameObject Fruit;
    public GameObject TreeFruit;
    public GameObject Spawner;
    public GameObject FruitLocation1;
    public GameObject FruitLocation2;
    public GameObject FruitLocation3;
    public GameObject FruitLocation4;
    public GameObject FruitLocation5;

    public float timer;
    public float fruitCount = 5;
    public float maxFruit = 5;

    public int respawnTime;

    // For carrot type "carrot" into the string
    // For grape type "grape" into the string

    private void Start()
    {
        if (tree == true)
        {
            Instantiate(TreeFruit, FruitLocation1.transform.position, FruitLocation1.transform.rotation);
            Instantiate(TreeFruit, FruitLocation2.transform.position, FruitLocation2.transform.rotation);
            Instantiate(TreeFruit, FruitLocation3.transform.position, FruitLocation3.transform.rotation);
            Instantiate(TreeFruit, FruitLocation4.transform.position, FruitLocation4.transform.rotation);
            Instantiate(TreeFruit, FruitLocation5.transform.position, FruitLocation5.transform.rotation);
        }
    }

    void Update()
    {

        if (ground == true)
        {
            if (active == true)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<MeshCollider>().enabled = true;

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
                gameObject.GetComponent<MeshCollider>().enabled = false;
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
