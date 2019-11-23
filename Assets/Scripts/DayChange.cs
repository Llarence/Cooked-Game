using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayChange : MonoBehaviour
{
	void Update(){
		//full day cycle 6 minutes
		transform.Rotate(Time.deltaTime, 0, 0);
		//make sure lighting doesn't shine upsidedown
		if(transform.eulerAngles.x >= 265 && transform.eulerAngles.x <= 355){
			GetComponent<Light>().intensity = 0;
		}else{
			GetComponent<Light>().intensity = 1;
		}
    }
}
