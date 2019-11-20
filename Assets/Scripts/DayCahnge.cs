using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCahnge : MonoBehaviour
{
	void Update(){
		//full day cycle 6 minutes
		transform.Rotate(0, Time.deltaTime, 0);
    }
}
