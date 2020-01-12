using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseAvocado : AIModule
{
    NavMeshAgent agent;
    Vector3 destination;
    float timer;

    void Start(){
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update(){
        if(timer > 1){
            timer = 0;
            GameObject nearestAvocado = FindClosestAvocado();
            if(nearestAvocado != null){
                destination = nearestAvocado.transform.position;
            }
            agent.destination = destination;
        }
        timer += Time.deltaTime;
    }

    public GameObject FindClosestAvocado(){
        List<GameObject> gos = new List<GameObject>();
        foreach(GameObject go in (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject))){
            if(go.GetComponent<DataHolder>()){
                if(go.GetComponent<DataHolder>().has<Food>()){
                    if(go.GetComponent<DataHolder>().get<Food>().foodName == "Avocado"){
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
