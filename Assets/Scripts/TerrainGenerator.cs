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
    GameObject cubeInst;
    MeshFilter meshF;
    int[,,] vertices;
    Vector3[] meshVertices;
    Vector3[] meshUvs;
    int[] meshTriPoints;
    List<int> newList;
    List<Vector3> newListVect;
    List<int> triList;
    List<Vector3> triVectList;
    int pointPos;
    Mesh mesh;

    void Start(){
        Generate();
    }

    void Generate(){
        //sets offsets because perlin noise is not random in unity so we have to move along the plane randomly
        float xOffset = Random.Range(-1000f, 1000f);
        float zOffset = Random.Range(-1000f, 1000f);

        //loops through all possible points and spawns a cubes where it should
        vertices = new int[size, height, size];
        for(int x = 0; x < size; x++){
            for(int y = 0; y < height; y++){
                for(int z = 0; z < size; z++){
                    vertices[x, y, z] = GenerationFunction((float)x, (float)y, (float)z, xOffset, zOffset, heightDeterioration, size, height, detail, amount);
                }
            }
        }
        for(int x = 0; x < size - 1; x++){
            for(int y = 0; y < height - 1; y++){
                for(int z = 0; z < size - 1; z++){
                    cubeInst = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                    triList = new List<int>();
                    triVectList = new List<Vector3>();
                    for(int i = 0; i < 8; i++){
                        pointPos = LookUpTable.triTable[vertices[x, y, z] * 128 +
                        vertices[x + 1, y, z] * 64 +
                        vertices[x, y, z + 1] * 32 +
                        vertices[x + 1, y, z + 1] * 16 +
                        vertices[x, y + 1, z] * 8 +
                        vertices[x + 1, y + 1, z] * 4 +
                        vertices[x, y + 1, z + 1] * 2 +
                        vertices[x + 1, y + 1, z + 1], i];
                        if (pointPos > -1){
                            triList.Add(pointPos);
                        }
                    }
                    foreach(int i in triList){
                        if(i == 0){triVectList.Add(new Vector3(x + 0.5f, y, z));}
                        if(i == 1){triVectList.Add(new Vector3(x, y + 0.5f, z));}
                        if(i == 2){triVectList.Add(new Vector3(x, y, z + 0.5f));}
                        if(i == 3){triVectList.Add(new Vector3(x + 0.5f, y, z + 1));}
                        if(i == 4){triVectList.Add(new Vector3(x + 1, y + 0.5f, z + 1));}
                        if(i == 5){triVectList.Add(new Vector3(x + 1, y, z + 0.5f));}
                        if(i == 6){triVectList.Add(new Vector3(x + 0.5f, y + 1, z));}
                        if(i == 7){triVectList.Add(new Vector3(x, y - 0.5f, z + 1));}
                        if(i == 8){triVectList.Add(new Vector3(x, y + 1, z + 0.5f));}
                        if(i == 9){triVectList.Add(new Vector3(x + 0.5f, y + 1, z + 1));}
                        if(i == 10){triVectList.Add(new Vector3(x + 1, y - 0.5f, z));}
                        if(i == 11){triVectList.Add(new Vector3(x + 1, y + 1, z + 0.5f));}
                    }
                    meshTriPoints = triList.ToArray();
                    meshVertices = removeDupsVect(triVectList);
                    
                    mesh = new Mesh();

                    mesh.vertices = meshVertices;
                    mesh.triangles = meshTriPoints;
                    cubeInst.GetComponent<MeshFilter>().mesh = mesh;
                }
            }
        }
    }

    //returns whether to spawn a cube or not
    int GenerationFunction(float x, float y, float z, float xOff, float zOff, float heightDet, int size, int height, float deta, float amou){
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
            return 1;
        }else{
            return 0;
        }
    }

    int[] removeDups(int[] array){
        newList = new List<int>();
        foreach(int i in array){
            if(newList.Contains(i) == false){
                newList.Add(i);
            }
        }
        return newList.ToArray();
    }

    Vector3[] removeDupsVect(List<Vector3> array){
        newListVect = new List<Vector3>();
        foreach(Vector3 i in array){
            if(newListVect.Contains(i) == false){
                newListVect.Add(i);
            }
        }
        return newListVect.ToArray();
    }
}