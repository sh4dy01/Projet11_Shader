using UnityEngine;

public class CollectibleItem : OutlineObject
{
    [SerializeField] GameObject FXToSpawn;
    
    private bool _isClicked;

    protected override void Awake()
    {
        base.Awake();
        
        CurrentOutlineMaterial = GameManager.Instance.ItemOutlineMaterial;

        _isClicked = false;
    }
    
    public void IsCollected()
    {
        _isClicked = true;
    }

    protected virtual void Collect(GameObject player)
    {
        
    }

    public virtual void OnTriggerStay(Collider other)
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
