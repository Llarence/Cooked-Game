using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public bool clicked;
    public bool mushroom;

    void Update()
    {
        if(clicked == true)
        {
            if(mushroom == true)
            {
                GameObject.Find("Player").GetComponent<Inventory>().mushrooms++;
                Destroy(gameObject);
            }
        }
    }
}
