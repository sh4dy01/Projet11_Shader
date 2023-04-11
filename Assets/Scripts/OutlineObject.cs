using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    protected Material CurrentOutlineMaterial;

    private MeshRenderer _meshRenderer;
    
    protected virtual void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (!_meshRenderer)
        {
            _meshRenderer = GetComponentInChildren<MeshRenderer>();
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
        Material[] mats = new Material[_meshRenderer.materials.Length + 1];

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            mats[i] = _meshRenderer.materials[i];
        }
        
        mats[^1] = CurrentOutlineMaterial;

        _meshRenderer.materials = mats;
    }
    private void RemoveOutlineMaterial()
    {
        Material[] mats = new Material[_meshRenderer.materials.Length - 1];

        for (int i = 0; i < mats.Length; i++)
        {
            mats[i] = _meshRenderer.materials[i];
        }

        _meshRenderer.materials = mats;
    }
}
