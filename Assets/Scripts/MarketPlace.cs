using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPlace : MonoBehaviour
{

    public bool selected;

    public Image menu;
    public Image Results;

    public Text Timer;
    public Text results;

    public GameObject manager;
    public GameObject Player;

    public int minutes = 20;
    
    public float Seconds;
    public float seconds = 60f;

    public string timer;

    // Update is called once per frame
    void Update()
    {
        if (selected == true)
        {
            menu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (seconds >= 0)
        {
            seconds -= 1 * Time.deltaTime;
        }
        else
        {
            minutes -= 1;
            seconds = 60;
        }

        if (minutes <= 0)
        {
            Results.gameObject.SetActive(true);
            results.text = "Gold: " + GameObject.Find("Player").GetComponent<Player>().Gold + " Points:" + GameObject.Find("Player").GetComponent<Player>().Gold * 5;
        }

        Seconds = Mathf.Round(seconds);

        timer = minutes + ":" + Seconds;

        Timer.text = timer;
    }
}
