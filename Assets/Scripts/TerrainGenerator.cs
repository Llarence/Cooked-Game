using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TerrainGenerator : MonoBehaviour
{

    public int chunkDistance;
    public int height;
    public float detail;
    public float heightDeterioration;
    public float amount;
	public float heightDeteriorationStartVal;
	public int heightDeteriorationBeginning;
	public int heighDeteriorationPrepStart;
	public GameObject terrain;
	public GameObject player;
	Vector3 playerChunkPos;
	float xOffset;
    float zOffset;
	List<GameObject> chunks = new List<GameObject>();
	List<Vector3> chunkPoses = new List<Vector3>();
	List<Mesh> meshes = new List<Mesh>();
	List<Vector3> terrainPoses = new List<Vector3>();
	Thread generate;

    void Start(){
		//sets offsets because perlin noise is not random in unity so we have to move along the plane randomly
        xOffset = Random.Range(-1000f, 1000f);
        zOffset = Random.Range(-1000f, 1000f);
		ThreadUpdateChunks();
		generate = new Thread(ThreadUpdateChunks);
    }

	void Update(){
		if(playerChunkPos.x != Mathf.RoundToInt(player.transform.position.x / 16) || playerChunkPos.z != Mathf.RoundToInt(player.transform.position.z / 16) && generate.IsAlive == false){
			playerChunkPos = new Vector3(Mathf.RoundToInt(player.transform.position.z / 16), 0, Mathf.RoundToInt(player.transform.position.z / 16));
			generate = new Thread(ThreadUpdateChunks);
			generate.Start();
		}
		AfterUpdate();
	}

	void ThreadUpdateChunks(){
		for(int x = -chunkDistance; x < chunkDistance + 1; x++){
            for(int z = -chunkDistance; z < chunkDistance + 1; z++){
				bool alreadySpawned = false;
				foreach(Vector3 chunk in chunkPoses){
					if((int)(chunk.x / 16) == Mathf.RoundToInt(playerChunkPos.x / 16) + x && (int)(chunk.z / 16) == Mathf.RoundToInt(playerChunkPos.z / 16) + z){
						print((Mathf.RoundToInt(playerChunkPos.x / 16) + x) + " " + (Mathf.RoundToInt(playerChunkPos.z / 16) + z));
						alreadySpawned = true;
					}
				}
				if(!alreadySpawned){
					chunkPoses.Add(new Vector3((Mathf.RoundToInt(playerChunkPos.x / 16) + x) * 16, 0, (Mathf.RoundToInt(playerChunkPos.x / 16) + z) * 16));
        			GenerateChunk(Mathf.RoundToInt(playerChunkPos.x / 16) + x, Mathf.RoundToInt(playerChunkPos.z / 16) + z);
				}
			}
		}
	}

	void AfterUpdate(){
		if(meshes.Count > 0){
			foreach(Mesh m in meshes){
				GameObject terrInst = Instantiate(terrain, terrainPoses[meshes.IndexOf(m)], Quaternion.identity);
				terrInst.GetComponent<MeshCollider>().sharedMesh = m;
				terrInst.GetComponent<MeshFilter>().mesh = m;
				terrInst.GetComponent<MeshFilter>().mesh.RecalculateNormals();
				chunks.Add(terrInst);
			}
			meshes.Clear();
			terrainPoses.Clear();
		}
		foreach(Vector3 chunk in chunkPoses.ToArray()){
			if((int)(chunk.x / 16) > Mathf.RoundToInt(playerChunkPos.x / 16) + chunkDistance * 10 || 
			(int)(chunk.x / 16) < Mathf.RoundToInt(playerChunkPos.x / 16) - chunkDistance * 10 || 
			(int)(chunk.z / 16) > Mathf.RoundToInt(playerChunkPos.z / 16) + chunkDistance * 10 || 
			(int)(chunk.z / 16) < Mathf.RoundToInt(playerChunkPos.z / 16) - chunkDistance * 10){
				chunks.Remove(chunks[chunkPoses.IndexOf(chunk)]);
				Destroy(chunks[chunkPoses.IndexOf(chunk)]);
				chunkPoses.Remove(chunk);
			}else{
				if((int)(chunk.x / 16) > Mathf.RoundToInt(playerChunkPos.x / 16) + chunkDistance || 
				(int)(chunk.x / 16) < Mathf.RoundToInt(playerChunkPos.x / 16) - chunkDistance || 
				(int)(chunk.z / 16) > Mathf.RoundToInt(playerChunkPos.z / 16) + chunkDistance || 
				(int)(chunk.z / 16) < Mathf.RoundToInt(playerChunkPos.z / 16) - chunkDistance){
					chunks[chunkPoses.IndexOf(chunk)].SetActive(false);
				}else{
					chunks[chunkPoses.IndexOf(chunk)].SetActive(true);
				}
			}
		}
	}

   	void GenerateChunk(int xOfChunk, int zOfChunk){
        //loops through all possible points and says where ground should be
        int[,,] vertices = new int[17, height, 17];
        for(int x = 0; x < 17; x++){
			for(int z = 0; z < 17; z++){
            	for(int y = 0; y < height; y++){
					if(y == 0){
						vertices[x, y, z] = 1;
					}else if(y == height - 1){
						vertices[x, y, z] = 0;
					}else{
						vertices[x, y, z] = GenerationChunkData((float)(x + xOfChunk * 16), (float)y, (float)(z + zOfChunk * 16), xOffset, zOffset, heightDeteriorationBeginning, heightDeteriorationStartVal, heightDeterioration, heighDeteriorationPrepStart, detail, amount);
					}
                }
            }
		}
		GenerateChunkMesh(xOfChunk, zOfChunk, vertices);
    }

    //returns whether to spawn a ground or not one is yes, zero is no
	int GenerationChunkData(float x, float y, float z, float xOff, float zOff, int heightDetBeg, float heightDetStartVal, float heightDet, int heightDetPrepStart, float deta, float amou){
        //sets up x, y, and z
        x = x * deta;
        y = y * deta;
        z = z * deta;

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

        //checks if the location should be ground
        //y * heightDet makes it so as the y gets bigger the chance of spawn gets smaller
		//there is a hold of on the heightDet by the top if statment
		if(y / deta >= heightDetPrepStart){
			if(y / deta >= heightDetBeg){
				if(ABC > amou + (((y / deta) - heightDetBeg) * heightDet) + heightDetStartVal){
		            return 1;
		        }else{
		            return 0;
				}
			}else{
				//make cave to surface transition smooth
				if(ABC > amou + (((y / deta) - heightDetPrepStart) * (heightDetStartVal / (heightDetBeg - heightDetPrepStart)))){
					return 1;
				}else{
					return 0;
				}
			}
		}else{
			if(ABC > amou){
				return 1;
			}else{
				return 0;
			}
		}
    }

	void GenerateChunkMesh(int xOfChunk, int zOfChunk, int[,,] vertices){
		List<int> triReList;
		List<int> triList;
		int pointPos;
		Mesh mesh;
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
		mesh = new Mesh();
		finalTri = new List<int>();
		finalVerts = new List<Vector3>();
		totalI = 0;
		for(int x = 0; x < 16; x++){
			for(int z = 0; z < 16; z++){
				for(int y = 0; y < height - 1; y++){
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
							permVerts.Add(new Vector3(x + tempVert.x - 8, y + tempVert.y, z + tempVert.z - 8));
							tempVert = tempVerts[triList[i + 1]];
							permVerts.Add(new Vector3(x + tempVert.x - 8, y + tempVert.y, z + tempVert.z - 8));
							tempVert = tempVerts[triList[i + 2]];
							permVerts.Add(new Vector3(x + tempVert.x - 8, y + tempVert.y, z + tempVert.z - 8));
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
		//starts new chunk
		mesh.vertices = finalVerts.ToArray();
		mesh.triangles = finalTri.ToArray();
		meshes.Add(mesh);
		terrainPoses.Add(new Vector3(xOfChunk * 16, 0, zOfChunk * 16));
	}
}