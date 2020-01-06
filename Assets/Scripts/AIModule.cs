using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIModule
{
    public Dictionary<string, dynamic> inputVars = new Dictionary<string, dynamic>();
    public Dictionary<string, dynamic> outputVars = new Dictionary<string, dynamic>();

    public virtual void wakeUp(GameObject gameObject){

    }

    public virtual void run(GameObject gameObject){

    }
}
