using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{

    public int size;
    public int height;
    public float detail;
    public float heightDeterioration;
    public float amount;
    public GameObject cube;

    void Start(){
        Generate();
    }

    void Generate(){
        //sets offsets because perlin noise is not random in unity so we have to move along the plane randomly
        float xOffset = Random.Range(-1000f, 1000f);
        float zOffset = Random.Range(-1000f, 1000f);

        //loops through all possible points and spawns a cubes where it should
        for(int x = 0; x < size; x++){
            for(int y = 0; y < height; y++){
                for(int z = 0; z < size; z++){
                    if(GenerationFunction((float)x, (float)y, (float)z, xOffset, zOffset, heightDeterioration, size, height, detail, amount)){
                        Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                    }
                }
            }
        }
    }

    //returns whether to spawn a cube or not
    bool GenerationFunction(float x, float y, float z, float xOff, float zOff, float heightDet, int size, int height, float deta, float amou){
        //sets up x, y, and z
        x = x / size * deta;
        y = y / height * deta;
        z = z / size * deta;

        //adds offset
        x += xOff;
        z += zOff;

        //makes 3D perlin noise
        float AB = Mathf.PerlinNoise(x, y);
        float AC = Mathf.PerlinNoise(x, z);
        float BC = Mathf.PerlinNoise(y, z);

        float BA = Mathf.PerlinNoise(y, x);
        float CA = Mathf.PerlinNoise(z, x);
        float CB = Mathf.PerlinNoise(z, y);

        float ABC = AB + AC + BC + BA + CA + CB;
        ABC /= 6;

        //checks if the cube is in a spawn zone
        // y * heightDet makes it so as the y gets bigger the chance of spawn gets smaller
        if(ABC > amou + y * heightDet){
            return true;
        }else{
            return false;
        }
    }
}