using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arthur_SimpleMeshGenerator : MonoBehaviour
{
    public Material _MeshMaterial;
    
    

    private void Start()
    {
        //MakeTriangle();
        MakeQuad();
        //MakeDoubleQuad();
    }

    private void MakeTriangle()
    {
        Vector3[] vertices = new Vector3[]
        {
            new(0, 0, 0),
            new(1f, 1.5f, 0),
            new(2f, 0, 0)
        };
        
        int[] indices = new int[]
        {
            0, 1, 2
        };

        Color[] colors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue
        };
        
        BuildMesh("Triangle", vertices, indices, null, colors);
    }

    private void MakeQuad()
    {   
        Vector3[] vertices = new Vector3[12];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 2, 0);
        vertices[2] = new Vector3(2, 2, 0);
        vertices[3] = new Vector3(2, 0, 0);
        
        vertices[6] = new Vector3(0, 0, 2);
        vertices[7] = new Vector3(0, 2, 2);

        int[] indices = new int[]
        {
            0, 1, 2, 0, 2, 3,
            0, 1, 6, 1, 7, 6
        };
        
        Color[] colors = new Color[]
        {
            Color.red,
            Color.green,
            Color.blue,
            Color.cyan,
            Color.yellow,
            Color.magenta,
            Color.red,
            Color.green,
            Color.yellow,
            Color.magenta,
            Color.red,
            Color.green
        };
        
        BuildMesh("Quad", vertices, indices, null, colors);
    }

    private void MakeDoubleQuad()
    {
        Vector3[] vertices = new Vector3[6];
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, 2, 0);
        vertices[2] = new Vector3(2, 2, 0);
        vertices[3] = new Vector3(2, 0, 0);
        
        int[] indices = new int[]
        {
            0, 1, 2, 0, 2, 3,
            0, 2, 1, 0, 3, 2
        };

        BuildMesh("Double Quad", vertices, indices);
    }

    protected void BuildMesh(string gameObjectName, Vector3[] vertices, int[] indices, Vector2[] uvs = null, Color[] colors = null)
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
        meshFilter.mesh.colors = colors;

        // Apply the material.
        meshRenderer.material = _MeshMaterial != null ? _MeshMaterial : new Material(Shader.Find("Universal Render Pipeline/Unlit"));
    }
}
