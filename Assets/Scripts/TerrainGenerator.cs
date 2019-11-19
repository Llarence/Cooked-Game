﻿using System.Collections;
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
    int[,,] vertices;
	List<int> triReList;
    List<int> triList;
    int pointPos;
    Mesh mesh;
    Vector2[] uvs;
	public Material[] mats;
	Vector3[] tempVerts = new Vector3[]{new Vector3(0, 0, 0.5f),
		new Vector3(0.5f, 0, 1),
		new Vector3(1, 0, 0.5f),
		new Vector3(0.5f, 0, 0),
		new Vector3(0, 1, 0.5f),
		new Vector3(0.5f, 1, 1),
		new Vector3(1, 1, 0.5f),
		new Vector3(0.5f, 1, 0),
        new Vector3(0, 0.5f, 0),
        new Vector3(0, 0.5f, 1),
        new Vector3(1, 0.5f, 1),
        new Vector3(1, 0.5f, 0)};

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
					if(x == 0 || x == size - 1 || z == 0 || z == size - 1 || y == 0 || y == size - 1){
						vertices[x, y, z] = 0;
					}else{
						vertices[x, y, z] = GenerationFunction((float)x, (float)y, (float)z, xOffset, zOffset, heightDeterioration, size, height, detail, amount);
					}
                }
            }
        }
        for(int x = 0; x < size - 1; x++){
            for(int y = 0; y < height - 1; y++){
                for(int z = 0; z < size - 1; z++){
                    triList = new List<int>();
					triReList = new List<int>();
                    for(int i = 0; i < 16; i++){
						pointPos = LookUpTable.triTable[vertices[x + 1, y + 1, z] * 128 +
	                        vertices[x + 1, y + 1, z + 1] * 64 +
							vertices[x, y + 1, z + 1] * 32 +
	                        vertices[x, y + 1, z] * 16 +
							vertices[x + 1, y, z] * 8 +	
	                        vertices[x + 1, y, z + 1] * 4 +
							vertices[x, y, z + 1] * 2 +
	                        vertices[x, y, z], i];
                        if (pointPos > -1){
                            triList.Add(pointPos);
                        }
                    }
                    
					for(int i = 0; i < triList.Count; i++){
						if(i % 3 == 0){
							triReList.Add(triList[i]);
							triReList.Add(triList[i + 2]);
							triReList.Add(triList[i + 1]);
						}
					}

                    mesh = new Mesh();

                    mesh.vertices = tempVerts;
                    mesh.triangles = triReList.ToArray();
					if(triReList.Count > 0){
	                    cubeInst = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
	                    cubeInst.GetComponent<MeshFilter>().mesh = mesh;
						cubeInst.GetComponent<MeshFilter>().mesh.RecalculateNormals();
						cubeInst.GetComponent<MeshRenderer>().material = mats[Random.Range(0, mats.Length)];
					}
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
}