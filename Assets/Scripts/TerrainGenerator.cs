using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Concurrent;
using System.Threading;

class meshDataAndPos{
	public Vector3[] verts;
	public int[] tris;
	public Vector3 pos;
}

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
	float xOffset;
    float zOffset;
	List<GameObject> chunks = new List<GameObject>();
	List<Vector3> chunkPoses = new List<Vector3>();
	ConcurrentBag<meshDataAndPos> meshesAndPoses = new ConcurrentBag<meshDataAndPos>();
	int threads = 0;

    void Start(){
		//sets offsets because perlin noise is not random in unity so we have to move along the plane randomly
        xOffset = Random.Range(-1000f, 1000f);
        zOffset = Random.Range(-1000f, 1000f);
    }

	void Update(){
		UpdateChunks();
		if(threads == 0){
			SpawnChunks();
		}
	}

	void SpawnChunks(){
		meshDataAndPos[] meshesAndPosesArr = meshesAndPoses.ToArray();
		for(int i = 0; i < meshesAndPosesArr.Length; i++){
			GameObject terr = Instantiate(terrain, meshesAndPosesArr[i].pos, Quaternion.identity);
			Mesh mesh = new Mesh();
			mesh.vertices = meshesAndPosesArr[i].verts;
			mesh.triangles = meshesAndPosesArr[i].tris;
			terr.GetComponent<MeshFilter>().mesh = mesh;
			terr.GetComponent<MeshFilter>().mesh.RecalculateNormals();
			terr.GetComponent<MeshCollider>().sharedMesh = mesh;
			chunks.Add(terr);
			meshesAndPoses.TryTake(out meshesAndPosesArr[i]);
		}
	}

	void UpdateChunks(){
		for(int x = -chunkDistance; x < chunkDistance + 1; x++){
            for(int z = -chunkDistance; z < chunkDistance + 1; z++){
				bool alreadySpawned = false;
				foreach(Vector3 chunkPos in chunkPoses){
					if((int)(chunkPos.x / 16) == Mathf.RoundToInt(player.transform.position.x / 16) + x && (int)(chunkPos.z / 16) == Mathf.RoundToInt(player.transform.position.z / 16) + z){
						alreadySpawned = true;
					}
				}
				if(!alreadySpawned){
					int tempX = Mathf.RoundToInt(player.transform.position.x / 16) + x;
					int tempZ = Mathf.RoundToInt(player.transform.position.z / 16) + z;
        			chunkPoses.Add(new Vector3((Mathf.RoundToInt(player.transform.position.x / 16) + x) * 16, 0, (Mathf.RoundToInt(player.transform.position.z / 16) + z) * 16));
					Thread genThr = new Thread(() => GenerateChunk(tempX, tempZ));
					threads++;
					genThr.Start();
				}
			}
		}
		foreach(GameObject chunk in chunks.ToArray()){
			if((int)(chunk.transform.position.x / 16) > Mathf.RoundToInt(player.transform.position.x / 16) + (chunkDistance * 16) || 
			(int)(chunk.transform.position.x / 16) < Mathf.RoundToInt(player.transform.position.x / 16) - (chunkDistance * 16) || 
			(int)(chunk.transform.position.z / 16) > Mathf.RoundToInt(player.transform.position.z / 16) + (chunkDistance * 16) || 
			(int)(chunk.transform.position.z / 16) < Mathf.RoundToInt(player.transform.position.z / 16) - (chunkDistance * 16)){
				chunks.Remove(chunk);
				chunkPoses.Remove(new Vector3(chunk.transform.position.x, 0, chunk.transform.position.z));
				Destroy(chunk);
			}else{
				if((int)(chunk.transform.position.x / 16) > Mathf.RoundToInt(player.transform.position.x / 16) + chunkDistance || 
				(int)(chunk.transform.position.x / 16) < Mathf.RoundToInt(player.transform.position.x / 16) - chunkDistance || 
				(int)(chunk.transform.position.z / 16) > Mathf.RoundToInt(player.transform.position.z / 16) + chunkDistance || 
				(int)(chunk.transform.position.z / 16) < Mathf.RoundToInt(player.transform.position.z / 16) - chunkDistance){
					chunk.SetActive(false);
				}else{
					chunk.SetActive(true);
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
		meshDataAndPos mAndp = new meshDataAndPos();
		List<int> triReList;
		List<int> triList;
		int pointPos;
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
		mAndp.verts = finalVerts.ToArray();
		mAndp.tris = finalTri.ToArray();
		mAndp.pos = new Vector3(xOfChunk * 16, 0, zOfChunk * 16);
		threads--;
		meshesAndPoses.Add(mAndp);
	}
}