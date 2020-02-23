using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionData", menuName = "ActionData")]
public class Action : ScriptableObject
{
    public string fun;

    public void act(GameObject gameObject, GameObject player){
        switch(fun){
            case "throw":
                throwObject(gameObject, player);
                break;
        }
    }

    void throwObject(GameObject gameObject, GameObject player){
        player.GetComponent<Player>().dropPickUp();
        gameObject.transform.LookAt(-player.transform.position);
        gameObject.GetComponent<Rigidbody>().velocity = (player.transform.position - gameObject.transform.position).normalized * -30;
    }
}