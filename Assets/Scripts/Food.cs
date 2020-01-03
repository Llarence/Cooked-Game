using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "FoodData")]
public class Food : ScriptableObject
{
    public float holdHeightOnGround;
    public float holdDistance;
    public float holdUp;
    public float holdDistanceGround;
    public float grabDistance;
}
