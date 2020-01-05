using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    public ScriptableObject[] datas;

    public bool has<T>(){
        foreach(ScriptableObject data in datas){
            if(data.GetType() == typeof(T)){
                return true;
            }
        }

        return false;
    }

    public dynamic get<T>(){
        foreach(ScriptableObject data in datas){
            if(data.GetType() == typeof(T)){
                return data;
            }
        }

        return null;
    }
}
