using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    public bool clicked;
    public bool mushroom;
    public float respawn;
    public float respawnTime = 120f;

    void Update()
    {
        respawn += 1 * Time.deltaTime;
        if (clicked == true)
        {
            if(mushroom == true)
            {
                GameObject.Find("Player").GetComponent<Inventory>().mushrooms++;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<MeshCollider>().enabled = false;
                respawn = 0f;
                clicked = false;
            }
        }
        else
        {
            if (respawn >= respawnTime)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                gameObject.GetComponent<MeshCollider>().enabled = true;
                respawn = 0f;
            }
        }
    }
}
