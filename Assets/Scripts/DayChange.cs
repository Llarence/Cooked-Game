﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChange : MonoBehaviour
{
	void Update(){
		//full day cycle 6 minutes
		transform.Rotate(Time.deltaTime, 0, 0);
    }
}
