using System.Collections;
using UnityEngine;

public class DamageableEnemy : DamageableEntity
{

    protected override void Awake()
    {
        base.Awake();

        CurrentOutlineMaterial = GameManager.Instance.EnemyOutlineMaterial;
    }
}
