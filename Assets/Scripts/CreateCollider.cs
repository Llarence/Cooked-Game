#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class CreateCollider : MonoBehaviour
{
    public bool update;
    public GameObject emptyCol;

    void Update(){
        if(!Application.isPlaying && update){
            update = false;
    
            List<Transform> children = transform.Cast<Transform>().ToList();
            foreach(Transform child in children){
                DestroyImmediate(child.gameObject);
            }
            
            Mesh gameObjectMesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

            for(int i = 0; i < gameObjectMesh.triangles.Length; i += 3){
                GameObject emptyColInst = Instantiate(emptyCol, transform.position, Quaternion.identity);
                emptyColInst.transform.parent = gameObject.transform;
                Mesh colMesh = new Mesh();
                Vector3[] verts = new Vector3[6];
                int[] tris = new int[6]{0, 1, 2, 3, 4, 5};

                Vector3[] realVerts = new Vector3[3];
                for(int j = 0; j < 3; j++){
                    realVerts[j] = gameObjectMesh.vertices[gameObjectMesh.triangles[i + j]];
                }
                Vector3 cross = Vector3.Cross(realVerts[0], realVerts[1]).normalized * 0.05f;
                for(int j = 0; j < 3; j++){
                    verts[j] = realVerts[j] + cross;
                }
                for(int j = 0; j < 3; j++){
                    verts[j + 3] = realVerts[j] - cross;
                }

                colMesh.vertices = verts;
                colMesh.triangles = tris;
                emptyColInst.GetComponent<MeshCollider>().convex = true;
                emptyColInst.GetComponent<MeshCollider>().sharedMesh = colMesh;
            }
        }
    }
}
#endif