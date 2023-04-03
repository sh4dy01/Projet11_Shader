using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderGenerator_Noah : SimpleMeshGenerator_Noah 
{
	[Range(3, 30)]
	public int Sides = 16;
	[Range(0.1f, 3f)]
	public float Radius = 1f;
	[Range(0.2f, 4f)]
	public float Height = 0.5f;

	public bool Recompute = true;

	void Start()
	{
		Make();
	}

	private void Update()
	{
		if (Recompute)
		{
			Recompute = false;
			Make();
		}
	}

	void Make()
	{
		List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> indices = new List<int>();

		float ANGLE_MULT = 2.0F * Mathf.PI / (float)Sides;
        for (int i = 0; i <= Sides; i++)
		{
			float a = i * ANGLE_MULT;
			vertices.Add(new Vector3(Mathf.Cos(a) * Radius, Height * 0.5F, Mathf.Sin(a) * Radius));
			normals.Add(new Vector3(Mathf.Cos(a), 0.0F, Mathf.Sin(a)));
		}
		for (int i = 0; i <= Sides; i++)
		{
			float a = i * ANGLE_MULT;
			vertices.Add(new Vector3(Mathf.Cos(a) * Radius, Height * -0.5F, Mathf.Sin(a) * Radius));
            normals.Add(new Vector3(Mathf.Cos(a), 0.0F, Mathf.Sin(a)));
        }

		for (int i = 0; i < Sides; ++i)
		{
			indices.Add(0+i); indices.Add(1+i);       indices.Add(Sides+1+i);
            indices.Add(1+i); indices.Add(Sides+2+i); indices.Add(Sides+1+i);
        }

		BuildMesh("Cylinder", vertices.ToArray(), indices.ToArray(), null, normals.ToArray());
	}
}
