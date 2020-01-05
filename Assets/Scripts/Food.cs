using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ReactantsAndProducts{
    public Food[] reactants;
    public Food[] products;
}

[CreateAssetMenu(fileName = "FoodData", menuName = "FoodData")]
public class Food : ScriptableObject
{
    public ReactantsAndProducts[] reactantsAndProductsInfo;
    public GameObject prefab;
}
