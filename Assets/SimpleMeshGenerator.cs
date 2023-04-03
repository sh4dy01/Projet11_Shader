using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMeshGenerator : MonoBehaviour
{
    public Material _MeshMaterial;
    


    void Start()
    {
    }

    void MakeTriangle()
    {
        Vector3[] vertices = new Vector3[3];
        int[] tab = new int[3];

        vertices[0] = new Vector3(0,0,0);
        vertices[1] = new Vector3(1,1,0);
        vertices[2] = new Vector3(2,0,0);

        tab[0] = 0;
        tab[1] = 1;
        tab[2] = 2;

        BuildMesh("test", vertices, tab);
    }

    void MakeCube()
    {
        Vector3[] vertices = new[]
        {
            new Vector3(0,0,0),
            new Vector3(0,1,0),
            new Vector3(1,1,0),
            new Vector3(1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,1,1),
            new Vector3(1,0,1),
            new Vector3(1,1,1)
        };
        int[] tab = new int[]
        {
            0,1,2,0,2,3,
            3,2,7,3,7,6,
            6,7,5,6,5,4,
            4,5,1,4,1,0,
            1,5,7,1,7,2,
            4,0,3,4,3,6
        };
        
        BuildMesh("cube", vertices, tab);
    }
    
    

    void MakeQuad()
    {
        Vector3[] vertices = new Vector3[4];
        int[] tab = new int[6]
        {
            0,1,2,0,3,1
        };

        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(1, 1, 0);
        vertices[2] = new Vector3(1, 0, 0);
        vertices[3] = new Vector3(0, 1, 0);
        
        

        BuildMesh("test", vertices, tab);
    }

    protected void BuildMesh(string gameObjectName, Vector3[] vertices, int[] indices, Vector2[] uvs = null)
    {
        // Search in the scene if there is a GameObject called "gameObjectName". If yes, we destroy it.
        GameObject oldOne = GameObject.Find(gameObjectName);
        if (oldOne != null)
            DestroyImmediate(oldOne);

        // Create a GameObject
        GameObject primitive = new GameObject(gameObjectName);
        
        // Add the components...
        MeshRenderer meshRenderer = primitive.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = primitive.AddComponent<MeshFilter>();
      
        // ... and set the mesh buffers. 
        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.triangles = indices;
        meshFilter.mesh.uv = uvs;

        // Apply the material.
        meshRenderer.material = _MeshMaterial != null ? _MeshMaterial : new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    }
}
