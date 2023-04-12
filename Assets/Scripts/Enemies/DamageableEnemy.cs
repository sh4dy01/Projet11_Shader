using System.Collections;
using UnityEngine;

public class DamageableEnemy : DamageableEntity
{
    [SerializeField] private Material _matBase;
    [SerializeField] private Material _matDamage;
    
    protected override void Awake()
    {
        base.Awake();

        CurrentOutlineMaterial = GameManager.Instance.EnemyOutlineMaterial;
    }

    protected override void GetHitEffect()
    {
        // Visual effect
        StartCoroutine(HitEffect());
    }

    IEnumerator HitEffect()
    {
        GetComponent<MeshRenderer>().material = _matDamage;
        yield return new WaitForSeconds(0.3f);
        GetComponent<MeshRenderer>().material = _matBase;
    }
}
