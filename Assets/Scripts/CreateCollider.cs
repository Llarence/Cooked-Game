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

    void Update(){
        if(!Application.isPlaying && update){
            update = false;
            List<Vector3> verts = new List<Vector3>();
            List<int[]> shapes = new List<int[]>();
            string path = "Assets/" + folderPath;
            string line;
            StreamReader reader = new StreamReader(path);
            while(!reader.EndOfStream){
                line = reader.ReadLine();
                if(line.StartsWith("v ")){
                    string[] dataClumps = line.Split(' ');
                    verts.Add(new Vector3(float.Parse(dataClumps[1]), float.Parse(dataClumps[2]), float.Parse(dataClumps[3])));
                }
                if(line.StartsWith("f ")){
                    string[] dataClumps = line.Remove(0, 2).Split(' ');
                    int[] shape = new int[dataClumps.Length];
                    for(int i = 0; i < dataClumps.Length; i++){
                        shape[i] = int.Parse(dataClumps[i].Split('/')[0]);
                    }
                    shapes.Add(shape);
                }
            }
            reader.Close();
        }
    }
}
#endif