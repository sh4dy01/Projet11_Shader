using UnityEngine;

public class Player : DamageableEntity
{
    [SerializeField] GameObject _shield;
    [SerializeField] GameObject _projectileToSpawn;
    [SerializeField] Transform _projectileSpawnLocation;
    
    private void Update()
    {
        //Shader.SetGlobalVector("_WorldSpacePlayerPos", transform.position);
        
        /*
        // Projectile
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject clone = Instantiate(_projectileToSpawn, _projectileSpawnLocation.transform.position, Quaternion.identity);
            clone.transform.forward = transform.forward;
        }

        // Shield
        if (Input.GetKeyDown(KeyCode.E))
            _shield.SetActive(!_shield.activeSelf);
            */
    }

    protected override void GetHitEffect()
    {
        // player Visual effect
    }
    
    protected override void Die()
    {
        // Respawn
    }
}
