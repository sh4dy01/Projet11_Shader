using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    protected Material CurrentOutlineMaterial;
    
    private List<Material> _materials;
    private MeshRenderer _meshRenderer;
    
    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (!_meshRenderer)
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
        }

        if (_meshRenderer)
        {
            _materials = _meshRenderer.materials.ToList();
        }

        
        CurrentOutlineMaterial = GameManager.Instance.OutlineMaterial;
    }
    
    private void OnMouseEnter()
    {
        if (!_meshRenderer) return;
        AddOutlineMaterial();
    }
    
    private void OnMouseExit()
    {
        if (!_meshRenderer) return;
        RemoveOutlineMaterial();
    }
    
    private void AddOutlineMaterial()
    {
        _materials.Add(CurrentOutlineMaterial);
        _meshRenderer.SetMaterials(_materials);
    }
    
    private void RemoveOutlineMaterial()
    {
        _materials.RemoveAt(_materials.Count - 1);
        _meshRenderer.materials = _materials.ToArray();
    }
}
