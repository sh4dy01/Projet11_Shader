using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arthur_TorusGenerator : SimpleMeshGenerator  
{
    [Range(3, 30)]
    public int Sides = 6;
    [Range(1f, 3f)]
    public float Radius = 1f;
    [Range(0.2f, 1f)]
    public float Height = 0.5f;

    public bool Recompute = false;

    private void Start()
    {
        MakeTorus();
    }

    private void Update()
    {
        if (Recompute)
        {
            Recompute = false;
            MakeTorus();
        }
    }
    
    private void MakeTorus()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> indices = new List<int>();

        float angle_mult = 2.0F * Mathf.PI / (float)Sides;
        for (int i = 0; i <= Sides; i++)
        {
            float a = i * angle_mult;
            vertices.Add(new Vector3(Mathf.Cos(a) * Radius, Height * 0.5F, Mathf.Sin(a) * Radius));
        }
        for (int i = 0; i <= Sides; i++)
        {
            float a = i * angle_mult;
            vertices.Add(new Vector3(Mathf.Cos(a) * Radius, Height * -0.5F, Mathf.Sin(a) * Radius));
        }

        for (int i = 0; i < Sides; ++i)
        {
            indices.Add(0+i); indices.Add(1+i);
            indices.Add(Sides+1+i);
            indices.Add(1+i); indices.Add(Sides+2+i);
            indices.Add(Sides+1+i);
        }

        BuildMesh("Torus", vertices.ToArray(), indices.ToArray());
    }
}
