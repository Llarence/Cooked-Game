#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class CreateCollider : MonoBehaviour
{
    public string folderPath;
    public bool update;
    string path;
    string line;

    void Update(){
        if(!Application.isPlaying && update){
            update = false;
            path = "Assets/" + folderPath;
            StreamReader reader = new StreamReader(path);
            while(!reader.EndOfStream){
                line = reader.ReadLine();
                if(line.StartsWith("v")){
                    print("v");
                }
                if(line.StartsWith("f")){
                    print("f");
                }
            }
            reader.Close();
        }
    }
}
#endif