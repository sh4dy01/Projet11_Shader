using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_TorusGenerator : SimpleMeshGenerator  
{
    [Range(3, 30)]
    public int TorusSides = 3;
    [Range(1f, 3f)]
    public float TorusRadius = 2f;
    [Range(0.2f, 1f)]
    public float TorusHeight = 0.5f;

    public bool RecomputeTorus = false;

    private void Start()
    {
        MakeTorus();
    }

    private void Update()
    {
        if (RecomputeTorus)
        {
            RecomputeTorus = false;
            MakeTorus();
        }
    }
    
    private void MakeTorus()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();

        for (int i = 0; i < TorusSides; i++)
        {
            vertices.Add(new Vector3(0, 0, 0));
        }

        BuildMesh("Torus", vertices.ToArray(), indices.ToArray());
    }
}
