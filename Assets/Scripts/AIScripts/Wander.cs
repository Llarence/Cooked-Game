using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : AIModule
{
    NavMeshAgent agent;
    Vector3 destination;
    float timer;

    public override void wakeUp(){
        agent = gameObject.GetComponent<NavMeshAgent>();
        timer = 5;
    }

    public override void run(){
        if((gameObject.transform.position - destination).sqrMagnitude < 6 || timer > 5){
            timer = 0;
            destination = gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-15f, 15f), 0, UnityEngine.Random.Range(-15f, 15f));
            agent.destination = destination;
        }
        timer += Time.deltaTime;
    }
}