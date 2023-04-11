using System;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] GameObject FXToSpawn;
    [ColorUsage(true, true)]
    [SerializeField] private Color _outlineColor;
    
    private Material _outlineMaterial;
    private MeshRenderer _meshRenderer;
    private bool _isClicked;

    private void Awake()
    {
        _isClicked = false;
        _meshRenderer = GetComponent<MeshRenderer>();
        _outlineMaterial = GameManager.Instance.OutlineMaterial;
        _outlineMaterial.color = _outlineColor;
    }
    
    public void IsCollected()
    {
        _isClicked = true;
    }

    protected virtual void Collect(GameObject player)
    {
        
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (_isClicked && other.CompareTag("Player"))
        {
            Collect(other.gameObject);
            
            if (FXToSpawn != null)
                Instantiate(FXToSpawn, gameObject.transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter()
    {
        AddOutlineMaterial();
    }
    
    private void OnMouseExit()
    {
        RemoveOutlineMaterial();
    }

    private void AddOutlineMaterial()
    {
        Material[] mats = new Material[_meshRenderer.materials.Length + 1];

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            mats[i] = _meshRenderer.materials[i];
        }

        mats[^1] = _outlineMaterial;

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
