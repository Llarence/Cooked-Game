using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Player>().openPickup != null)
        {
            if (gameObject.GetComponent<Player>().openPickup.GetComponent<Rigidbody>().useGravity == false)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    gameObject.GetComponent<Player>().openPickup.GetComponent<SellableObject>().store = true;
                }
            }
        }
    }
}
