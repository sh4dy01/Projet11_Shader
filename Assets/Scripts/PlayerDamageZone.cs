using UnityEngine;

public class PlayerDamageZone : DamageZone
{
    protected override void OnTriggerStay(Collider other)
    {
        Debug.Log(_canDamage);
        Debug.Log(other.gameObject.name);
        
        if (!_canDamage || !other.gameObject.CompareTag("Enemy")) return;
        
        Debug.Log("Damaging enemy");
        DamageEntity(other);
    }
}
