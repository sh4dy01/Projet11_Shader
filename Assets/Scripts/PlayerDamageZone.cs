using UnityEngine;

public class PlayerDamageZone : DamageZone
{
    protected override void OnTriggerStay(Collider other)
    {
        if (!_canDamage || !other.gameObject.CompareTag("Enemy")) return;
        
        Debug.Log("Damaging enemy");
        DamageEntity(other);
    }
}
