using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _dissolveRate = 0.0125f;
    [SerializeField] private float _refreshRate = 0.025f;
    
    private static readonly int DissolveAmount = Shader.PropertyToID("_DissolveAmount");

    // Start is called before the first frame update
    private void Start()
    {
        if (!_meshRenderer) return;
        
        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _meshRenderer.materials[i] = new Material(_meshRenderer.materials[i]);
            _meshRenderer.materials[i].SetFloat(DissolveAmount, 0);
        }
    }
    
    public void Dissolve(Action callback)
    {
        StartCoroutine(DissolveCo(callback));
    }

    private IEnumerator DissolveCo(Action callback)
    {
        yield return new WaitForSeconds(0.5f);
        
        if (_meshRenderer.materials.Length > 0)
        {
            float counter = 0;
            
            while (_meshRenderer.materials[0].GetFloat(DissolveAmount) < 1)
            {
                counter += _dissolveRate;

                foreach (var material in _meshRenderer.materials)
                {
                    material.SetFloat(DissolveAmount, counter);
                }
                
                yield return new WaitForSeconds(_refreshRate);
            }
        }
        
        callback?.Invoke();
    }
}
