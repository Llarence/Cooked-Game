using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public GameObject InventoryUI;
    public GameObject Player;
    public GameObject MarketMenu;

    public Text GoldText;

    public Transform SpawnLocation;

    public List<GameObject> Foods = new List<GameObject>();
    public List<float> FoodCount = new List<float>(); //The element for this corresponds with the element for the actual gameobject (i.e if element 0 is banana for Foods then element 0 for FoodCount is ammount of bananas)
    public List<Text> UIbuttonTexts = new List<Text>();
    public List<Text> UImarketTexts = new List<Text>();
    public List<int> FoodPrices = new List<int>();

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
        UIbuttonTexts[7].text = "Pear: " + FoodCount[7];
        UIbuttonTexts[8].text = "Garlic: " + FoodCount[8];
        UIbuttonTexts[9].text = "Corn: " + FoodCount[9];
        UIbuttonTexts[10].text = "Bread: " + FoodCount[10];

        UImarketTexts[0].text = "Bananas: " + FoodCount[0];
        UImarketTexts[1].text = "Carrots: " + FoodCount[1];
        UImarketTexts[2].text = "Fish #1: " + FoodCount[2];
        UImarketTexts[3].text = "Fish #2: " + FoodCount[3];
        UImarketTexts[4].text = "Grapes: " + FoodCount[4];
        UImarketTexts[5].text = "Apples: " + FoodCount[5];
        UImarketTexts[6].text = "FruitSalad: " + FoodCount[6];
        UImarketTexts[7].text = "Pear: " + FoodCount[7];
        UImarketTexts[8].text = "Garlic: " + FoodCount[8];
        UImarketTexts[9].text = "Corn: " + FoodCount[9];
        UImarketTexts[10].text = "Bread: " + FoodCount[10];

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
    public void pear()
    {
        if (FoodCount[7] > 0)
        {
            Instantiate(Foods[7], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[7]--;
        }
    }
    public void garlic()
    {
        if (FoodCount[8] > 0)
        {
            Instantiate(Foods[8], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[8]--;
        }
    }
    public void corn()
    {
        if (FoodCount[9] > 0)
        {
            Instantiate(Foods[9], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[9]--;
        }
    }
    public void bread()
    {
        if (FoodCount[10] > 0)
        {
            Instantiate(Foods[10], SpawnLocation.position, SpawnLocation.rotation);
            FoodCount[10]--;
        }
    }

    public void bananasMarket()
    {
        if (FoodCount[0] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[0];
            FoodCount[0]--;
        }
    }
    public void carrotsMarket()
    {
        if (FoodCount[1] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[1];
            FoodCount[1]--;
        }
    }
    public void fish_001Market()
    {
        if (FoodCount[2] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[2];
            FoodCount[2]--;
        }
    }
    public void fish_002Market()
    {
        if (FoodCount[3] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[3];
            FoodCount[3]--;
        }
    }
    public void grapesMarket()
    {
        if (FoodCount[4] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[4];
            FoodCount[4]--;
        }
    }
    public void applesMarket()
    {
        if (FoodCount[5] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[5];
            FoodCount[5]--;
        }
    }
    public void fruitSaladMarket()
    {
        if (FoodCount[6] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[6];
            FoodCount[6]--;
        }
    }
    public void pearMarket()
    {
        if (FoodCount[7] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[7];
            FoodCount[7]--;
        }
    }
    public void garlicMarket()
    {
        if (FoodCount[8] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[8];
            FoodCount[8]--;
        }
    }
    public void cornMarket()
    {
        if (FoodCount[9] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[9];
            FoodCount[9]--;
        }
    }
    public void breadMarket()
    {
        if (FoodCount[10] != 0)
        {
            Player.GetComponent<Player>().Gold += FoodPrices[10];
            FoodCount[10]--;
        }
    }
    public void BackMarket()
    {
        GameObject.Find("Market").GetComponent<MarketPlace>().selected = false;
        MarketMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
