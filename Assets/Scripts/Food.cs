using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct reactantsAndProducts{
    public Food[] reactants;
    public Food[] products;
}

[CreateAssetMenu(fileName = "FoodData", menuName = "FoodData")]
public class Food : ScriptableObject
{
    public reactantsAndProducts[] reactantsAndProductsInfo;
}
