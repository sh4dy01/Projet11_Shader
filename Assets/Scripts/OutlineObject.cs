using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    protected Material CurrentOutlineMaterial;
    
    protected List<Material> _materials;
    private MeshRenderer _meshRenderer;
    
    protected virtual void Awake()
    {
        _materials = new List<Material>();
        GetMaterials();
        
        CurrentOutlineMaterial = GameManager.Instance.OutlineMaterial;
    }

    protected virtual void GetMaterials()
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
    }

    private void OnMouseEnter()
    {
        if (_materials.Count <= 0) return;
        AddOutlineMaterial();
    }
    
    private void OnMouseExit()
    {
        if (_materials.Count <= 0) return;
        RemoveOutlineMaterial();
    }
    
    protected virtual void AddOutlineMaterial()
    {
        _materials.Add(CurrentOutlineMaterial);
        _meshRenderer.SetMaterials(_materials);
    }
    
    protected virtual void RemoveOutlineMaterial()
    {
        _materials.RemoveAt(_materials.Count - 1);
        _meshRenderer.materials = _materials.ToArray();
    }
}
