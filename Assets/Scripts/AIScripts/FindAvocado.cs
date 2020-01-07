﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAvocado : AIDetectModule
{
    public override void wakeUp(){

    }

    public override void detect(){
        GameObject nearestAvocado = FindClosestAvocado();
        if(nearestAvocado != null){
            vars["ClosestAvocadoDistanceSquared"] = (nearestAvocado.transform.position - gameObject.transform.position).sqrMagnitude;
        }else{
            vars["ClosestAvocadoDistanceSquared"] = Mathf.Infinity;
        }
    }

    public GameObject FindClosestAvocado(){
        List<GameObject> gos = new List<GameObject>();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Untagged")){
            if(go.GetComponent<DataHolder>()){
                if(go.GetComponent<DataHolder>().has<Food>()){
                    if(go.GetComponent<DataHolder>().get<Food>().name == "Avocado"){
                        gos.Add(go);
                    }
                }
            }
        }
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = gameObject.transform.position;

        foreach (GameObject go in gos){
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance){
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
