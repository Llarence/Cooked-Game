using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collisionator : MonoBehaviour
{
    Food myFood = null;
    List<Food> foodsTouching = new List<Food>();
    List<GameObject> gameObjectFoodsTouching = new List<GameObject>();

    void Start(){
        myFood = GetComponent<DataHolder>().get<Food>();
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.GetComponent<DataHolder>() != null && col.gameObject.layer != 8 && gameObject.layer != 8){
            if(col.gameObject.GetComponent<DataHolder>().has<Food>()){
                foodsTouching.Add(col.gameObject.GetComponent<DataHolder>().get<Food>());
                gameObjectFoodsTouching.Add(col.gameObject);
                
                foreach(ReactantsAndProducts recipe in myFood.reactantsAndProductsInfo){
                    bool recipeWorks = true;
                    foreach(Food food in recipe.reactants){
                        if(!foodsTouching.Contains(food)){
                            recipeWorks = false;
                        }
                    }

                    if(recipeWorks){
                        foreach(Food product in recipe.products){
                            Instantiate(product.prefab, transform.position, Quaternion.identity);
                        }
                        
                        GameObject[] tempFoods = gameObjectFoodsTouching.ToArray();
                        foreach(GameObject food in tempFoods){
                            Food foodData = food.GetComponent<DataHolder>().get<Food>();
                            if(recipe.reactants.Contains(foodData)){
                                foodsTouching.Remove(foodData);
                                gameObjectFoodsTouching.Remove(food);
                                food.layer = 8;
                                Destroy(food);
                            }
                        }


                        gameObject.layer = 8;
                        Destroy(gameObject);

                        break;
                    }
                }
            }
        }
    }

    void OnCollisionExit(Collision col){
        if(col.gameObject.GetComponent<DataHolder>() != null && col.gameObject.layer != 8 && gameObject.layer != 8){
            if(col.gameObject.GetComponent<DataHolder>().has<Food>()){
                foodsTouching.Remove(col.gameObject.GetComponent<DataHolder>().get<Food>());
                gameObjectFoodsTouching.Remove(col.gameObject);
            }
        }
    }
}
