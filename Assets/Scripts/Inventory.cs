using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject InventoryUI;

    public Text GoldText;

    public Transform SpawnLocation;

    public List<GameObject> Foods = new List<GameObject>();
    public List<float> FoodCount = new List<float>(); //The element for this corresponds with the element for the actual gameobject (i.e if element 0 is banana for Foods then element 0 for FoodCount is ammount of bananas)
    public List<Text> UIbuttonTexts = new List<Text>();

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

        UIbuttonTexts[0].text = "Bananas: " + FoodCount[0];
        UIbuttonTexts[1].text = "Carrots: " + FoodCount[1];
        UIbuttonTexts[2].text = "Fish #1: " + FoodCount[2];
        UIbuttonTexts[3].text = "Fish #2: " + FoodCount[3];
        UIbuttonTexts[4].text = "Grapes: " + FoodCount[4];
        UIbuttonTexts[5].text = "Apples: " + FoodCount[5];
        UIbuttonTexts[6].text = "FruitSalad: " + FoodCount[6];

        GoldText.text = "Gold: " + GameObject.Find("Player").GetComponent<Player>().Gold;

    }

    public void bananas()
    {
        if (FoodCount[0] > 0)
        {
            Instantiate(Foods[0], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[0]--;
        }
    }
    public void carrots()
    {
        if (FoodCount[1] > 0)
        {
            Instantiate(Foods[1], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[1]--;
        }
    }
    public void fish_001()
    {
        if (FoodCount[2] > 0)
        {
            Instantiate(Foods[2], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[2]--;
        }
    }
    public void fish_002()
    {
        if (FoodCount[3] > 0)
        {
            Instantiate(Foods[3], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[3]--;
        }
    }
    public void grapes()
    {
        if (FoodCount[4] > 0)
        {
            Instantiate(Foods[4], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[4]--;
        }
    }
    public void apples()
    {
        if (FoodCount[5] > 0)
        {
            Instantiate(Foods[5], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[5]--;
        }
    }
    public void fruitSalad()
    {
        if (FoodCount[6] > 0)
        {
            Instantiate(Foods[6], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[6]--;
        }
    }
}
