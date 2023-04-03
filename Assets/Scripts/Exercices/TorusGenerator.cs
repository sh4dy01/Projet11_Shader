using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusGenerator : SimpleMeshGenerator  
{
    [Range(3, 30)]
    public int TorusSides = 3;
    [Range(1f, 3f)]
    public float TorusRadius = 2f;
    [Range(0.2f, 1f)]
    public float TorusHeight = 0.5f;

    public bool RecomputeTorus = false;

    void Start()
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

    
    int[] GetQuadIndices(int intOffset, int modulo)
    {
        return new int[]
        {
        (0 + intOffset) % modulo,
        (1 + intOffset) % modulo,
        (2 + intOffset) % modulo,
        (1 + intOffset) % modulo,
        (3 + intOffset) % modulo,
        (2 + intOffset) % modulo,
        (0 + intOffset) % modulo,
        (2 + intOffset) % modulo,
        (1 + intOffset) % modulo,
        (1 + intOffset) % modulo,
        (2 + intOffset) % modulo,
        (3 + intOffset) % modulo
        };
    }

    void MakeTorus()
    {
        var vertices = new List<Vector3>();
        var indices = new List<int>();

        float angle = (2 * Mathf.PI) / TorusSides;
        for (int i = 1; i <= TorusSides; i++)
        {
            float cos = Mathf.Cos(angle*i)*TorusRadius;
            float sin = Mathf.Sin(angle*i)*TorusRadius;
            
            vertices.Add(new Vector3(cos,0,sin));
            vertices.Add(new Vector3(cos,TorusHeight,sin));

            var tab = GetQuadIndices((i - 1) * 2,TorusSides*2);
            foreach (var number in tab) indices.Add(number);
        }
        
        BuildMesh("Torus", vertices.ToArray(), indices.ToArray());
    }
}
