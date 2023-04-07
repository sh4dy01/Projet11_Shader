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

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isClicked)
        {
            if (FXToSpawn != null)
                Instantiate(FXToSpawn, gameObject.transform.position, Quaternion.identity);
        }
    }

}
