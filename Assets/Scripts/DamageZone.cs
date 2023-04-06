using System.Collections;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] protected float _damageDelay = 1f;

    protected bool _canDamage;
    private int _damageToDeal;

    // Start is called before the first frame update
    private void Awake()
    {
        _damageToDeal = GetComponentInParent<DamageableEntity>().Damage;
        _canDamage = true;
    }
    
    protected virtual void OnTriggerStay(Collider other) {}
    
    private IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(_damageDelay);
        _canDamage = true;
    }
    
    protected void DamageEntity(Collider other)
    {
        if (!other.TryGetComponent(out DamageableEntity entity)) return;

        entity.TakeDamage(_damageToDeal);
        _canDamage = false;
        StartCoroutine(ResetDamage());
    }
}
