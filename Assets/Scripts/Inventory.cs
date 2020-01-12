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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetItemValues()
    {
        Mushrooms.text = "Mushrooms: " + mushrooms;
    }
}
