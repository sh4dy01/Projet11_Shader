using System;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] GameObject FXToSpawn;
    private bool _isClicked;

    private void Awake()
    {
        _isClicked = false;
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

}
