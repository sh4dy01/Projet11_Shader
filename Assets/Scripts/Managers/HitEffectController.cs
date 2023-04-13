using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectController : MonoBehaviour
{
    [Header("Hit effect")]
    [SerializeField] private Material _matBase;
    [SerializeField] private Material _matDamage;
    [SerializeField] private float _hitEffectDuration = 0.3f;
    [SerializeField] private MeshRenderer _meshRenderer;

    public void StartEffect()
    {
        StartCoroutine(StartEffectEnumerator());
    }

    private IEnumerator StartEffectEnumerator()
    {
        _meshRenderer.material = _matDamage;
        yield return new WaitForSeconds(_hitEffectDuration);
        _meshRenderer.material = _matBase;
    }
}
