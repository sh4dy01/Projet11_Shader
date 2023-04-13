using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[ExecuteInEditMode]
public class WaterPlaneGenerator : MonoBehaviour 
{
	private MeshFilter _meshFilter;
	private MeshRenderer _meshRenderer;

	public int Subdivisions = 16;
	public float HalfExtents = 500.0F;


	private void Awake()
	{
		_meshFilter = GetComponent<MeshFilter>();
		_meshRenderer = GetComponent<MeshRenderer>();
		Make();
	}

	void Make()
	{
		List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> indices = new List<int>();

		float fsubs = Subdivisions;
		for (int z = 0; z <= Subdivisions; z++)
		{
			for (int x = 0; x <= Subdivisions; x++)
            {
				vertices.Add(new Vector3((-0.5F + x / fsubs) * HalfExtents * 2, 0.0F, (-0.5F + z / fsubs) * HalfExtents * 2));
				normals.Add(new Vector3(0, 1, 0));
			}
		}

		for (int z = 0; z < Subdivisions; z++)
		{
			int b = (Subdivisions + 1) * z;
			for (int x = 0; x < Subdivisions; x++)
			{
				indices.Add(b + x); indices.Add(b + x + Subdivisions + 1); indices.Add(b + x + 1);
				indices.Add(b + x + Subdivisions + 1); indices.Add(b + x + Subdivisions + 2); indices.Add(b + x + 1);
			}
		}


        // ... and set the mesh buffers. 
        _meshFilter.mesh.vertices = vertices.ToArray();
        _meshFilter.mesh.normals = normals.ToArray();
        _meshFilter.mesh.triangles = indices.ToArray();

        // Apply the material.
        //_meshRenderer.material = _MeshMaterial != null ? _MeshMaterial : new Material(Shader.Find("Learning/Unlit/Shader_Noah"));
	}
}
