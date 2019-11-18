using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int size;
    public int height;
    public GameObject cube;

    void Start(){
        Generate();
    }

    void Generate(){
        for(int x = 0; x < size; x++){
            for(int y = 0; y < height; y++){
                for(int z = 0; z < size; z++){
                    if(GenerationFunction((float)x, (float)y, (float)z, 0, 0, 0.3f, size, height, 3, 0.3f)){
                         
                    }
                }
            }
        }
    }

    bool GenerationFunction(float x, float y, float z, int xOff, int zOff, float heightDet, int size, int height, int detail, float amount){
        x += xOff;
        z += zOff;

        x = x / size * detail;
        y = y / height * detail;
        z = z / size * detail;

        float AB = Mathf.PerlinNoise(x, y);
        float AC = Mathf.PerlinNoise(x, z);
        float BC = Mathf.PerlinNoise(y, z);

        float BA = Mathf.PerlinNoise(y, x);
        float CA = Mathf.PerlinNoise(z, x);
        float CB = Mathf.PerlinNoise(z, y);

        float ABC = AB + AC + BC + BA + CA + CB;
        ABC /= 6;

        if(ABC > amount + y * heightDet){
            return true;
        }else{
            return false;
        }
    }
}