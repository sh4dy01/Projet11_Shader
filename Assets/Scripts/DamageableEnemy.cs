public class DamageableEnemy : DamageableEntity
{
    protected override void Awake()
    {
        base.Awake();

        CurrentOutlineMaterial = GameManager.Instance.EnemyOutlineMaterial;
    }

    protected override void GetHitEffect()
    {
        // Visual effect
    }
}
