﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIModule
{
    public Dictionary<string, dynamic> vars = new Dictionary<string, dynamic>();
    public GameObject gameObject;

    public virtual void wakeUp(){

    }

    public virtual void run(){

    }
}
