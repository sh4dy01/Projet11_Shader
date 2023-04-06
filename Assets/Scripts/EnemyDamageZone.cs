using UnityEngine;

public class EnemyDamageZone : DamageZone
{
    protected override void OnTriggerStay(Collider other)
    {
        if (!_canDamage || !other.gameObject.CompareTag("Player")) return;
        DamageEntity(other);
    }
}
