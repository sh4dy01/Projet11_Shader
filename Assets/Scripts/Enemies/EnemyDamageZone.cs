using UnityEngine;

public class EnemyDamageZone : DamageZone
{
    protected override void OnTriggerStay(Collider other)
    {
        if (!_canDamage) return;
        if (!other.gameObject.CompareTag("Player")) return;
        
        DamageEntity(other);
    }
}
