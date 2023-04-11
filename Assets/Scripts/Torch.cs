using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Torch : MonoBehaviour
{
    [SerializeField] private Light _pointLight;
    private void Update()
    {
        _pointLight.intensity = Random.Range(2.0F, 2.4F);
    }
}
