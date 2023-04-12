using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _dissolveRate = 0.0125f;
    [SerializeField] private float _refreshRate = 0.025f;
    
    private Material[] _materials;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (_meshRenderer)
        {
            _materials = _meshRenderer.materials;
        }
    }
    
    public void Dissolve(Action callback)
    {
        StartCoroutine(DissolveCo(callback));
    }

    private IEnumerator DissolveCo(Action callback)
    {
        if (_materials.Length > 0)
        {
            float counter = 0;
            
            while (_materials[0].GetFloat("_DissolveAmount") < 1)
            {
                counter += _dissolveRate;

                foreach (var material in _materials)
                {
                    material.SetFloat("_DissolveAmount", counter);
                }

                yield return new WaitForSeconds(_refreshRate);
            }
        }
        
        callback?.Invoke();
    }
}
