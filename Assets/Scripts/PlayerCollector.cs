using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{

    RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 4))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.GetComponent<Collectable>() != null)
                {
                    hit.collider.gameObject.GetComponent<Collectable>().selected = true;
                }
            }
        }
    }
}
