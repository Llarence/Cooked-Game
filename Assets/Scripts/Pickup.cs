using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickUpData", menuName = "PickUpData")]
public class PickUp : ScriptableObject
{
    public float holdHeightOnGround;
    public float holdDistance;
    public float holdUp;
    public float holdDistanceGround;
    public float grabDistance;
}
