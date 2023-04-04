using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusGenerator : SimpleMeshGenerator  
{
    [Range(3, 100)]
    public int TorusSides = 3;
    [Range(0f, 5f)]
    public float TorusRadius = 2f;
    [Range(0f, 5f)]
    public float TorusUpperRadius = 2f;
    [Range(0.2f, 5f)]
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
        var colors = new List<Color>();

        float angle = (2 * Mathf.PI) / TorusSides;
        for (int i = 1; i <= TorusSides; i++)
        {
            vertices.Add(new Vector3(Mathf.Cos(angle*i)*TorusRadius,0,Mathf.Sin(angle*i)*TorusRadius));
            vertices.Add(new Vector3(Mathf.Cos(angle*i)*TorusUpperRadius,TorusHeight,Mathf.Sin(angle*i)*TorusUpperRadius));
            colors.Add(new Color(0,0.02F*i,1));
            colors.Add(new Color(0.02F*i,1,1));
            
            var tab = GetQuadIndices((i - 1) * 2,TorusSides*2);
            foreach (var number in tab) indices.Add(number);
            
        }

        BuildMesh("Torus", vertices.ToArray(), indices.ToArray(), null, colors.ToArray());
    }
}
