using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public float Bananas;
    public float Carrots;
    public float Fish_001;
    public float Fish_002;
    public float Grapes;

    public GameObject InventoryUI;
    public GameObject BananasGameObject;
    public GameObject CarrotsGameObject;
    public GameObject Fish_001GameObject;
    public GameObject Fish_002GameObject;
    public GameObject GrapesGameObject;

    public Transform Player;

    public bool On = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && On == false)
        {
            InventoryUI.SetActive(true);
            On = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && On == true)
        {
            InventoryUI.SetActive(false);
            On = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void bananas()
    {
        Instantiate(BananasGameObject, Player.position, Player.rotation);
    }
}
