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
    int[,,] vertices;
	List<int> triReList;
    List<int> triList;
    int pointPos;
    Mesh mesh;
    Vector2[] uvs;
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
	List<Vector3> permVerts;
	List<int> finalTri;
	List<Vector3> finalVerts;
	Vector3 tempVert;
	int totalI;

    void Start(){
        Generate();
    }

    void Generate(){
        //sets offsets because perlin noise is not random in unity so we have to move along the plane randomly
        float xOffset = Random.Range(-1000f, 1000f);
        float zOffset = Random.Range(-1000f, 1000f);

        //loops through all possible points and says where ground should be
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
		mesh = new Mesh();
		finalTri = new List<int>();
		finalVerts = new List<Vector3>();
		totalI = 0;
        for(int x = 0; x < size - 1; x++){
            for(int y = 0; y < height - 1; y++){
                for(int z = 0; z < size - 1; z++){
					//gets the triangle points form the look up table
                    triList = new List<int>();
					triReList = new List<int>();
					permVerts = new List<Vector3>();
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
                    
					//makes traingles face outwards and adds the vertices for the traingles
					for(int i = 0; i < triList.Count; i++){
						if(i % 3 == 0){
							triReList.Add(totalI + i);
							triReList.Add(totalI + i + 2);
							triReList.Add(totalI + i + 1);
							tempVert = tempVerts[triList[i]];
							permVerts.Add(new Vector3(x + tempVert.x, y + tempVert.y, z + tempVert.z));
							tempVert = tempVerts[triList[i + 1]];
							permVerts.Add(new Vector3(x + tempVert.x, y + tempVert.y, z + tempVert.z));
							tempVert = tempVerts[triList[i + 2]];
							permVerts.Add(new Vector3(x + tempVert.x, y + tempVert.y, z + tempVert.z));
						}
					}

					//if the mesh is more than about the max vertices then it starts a new mesh and finishes the last one
					if(65500 < finalVerts.Count + permVerts.Count){
						mesh.vertices = finalVerts.ToArray();
						mesh.triangles = finalTri.ToArray();
						triReList = new List<int>();
						cubeInst = Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
						cubeInst.GetComponent<MeshFilter>().mesh = mesh;
						cubeInst.GetComponent<MeshFilter>().mesh.RecalculateNormals();
						mesh = new Mesh();
						finalTri = new List<int>();
						finalVerts = new List<Vector3>();
						totalI = 0;
						for(int i = 0; i < triList.Count; i++){
							if(i % 3 == 0){
								triReList.Add(totalI + i);
								triReList.Add(totalI + i + 2);
								triReList.Add(totalI + i + 1);
							}
						}
					}
					totalI += triList.Count;

					//add the triangles and vertices to the final lists
					if(triReList.Count > 0){
						foreach(Vector3 vect in permVerts){
							finalVerts.Add(vect);
						}
						foreach(int i in triReList){
							finalTri.Add(i);
						}
					}
                }
            }
        }
		//adds the final lists to the final mesh
		mesh.vertices = finalVerts.ToArray();
		mesh.triangles = finalTri.ToArray();
		cubeInst = Instantiate(cube, new Vector3(0, 0, 0), Quaternion.identity);
		cubeInst.GetComponent<MeshFilter>().mesh = mesh;
		cubeInst.GetComponent<MeshFilter>().mesh.RecalculateNormals();
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