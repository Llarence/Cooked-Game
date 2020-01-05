using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject inventory;
    public bool InventoryEnabled;
    public float mushrooms;
    public Text Mushrooms;

    void Update()
    {
        AccessInventory();

        SetItemValues();
    }

    public void AccessInventory()
    {
        if (Input.GetKeyDown(KeyCode.E) && InventoryEnabled == false)
        {
            InventoryEnabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && InventoryEnabled == true)
        {
            InventoryEnabled = false;
        }

        if(InventoryEnabled == true)
        {
            inventory.SetActive(true);
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    public void SetItemValues()
    {
        Mushrooms.text = "Mushrooms: " + mushrooms;
    }
}
