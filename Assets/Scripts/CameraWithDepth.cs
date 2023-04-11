using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWithDepth : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }
}
