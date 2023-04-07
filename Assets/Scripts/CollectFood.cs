using System;
using UnityEngine;

public class CollectFood : CollectibleItem
{
    [SerializeField] private PlayerHungerThirst playerStats;
    
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        playerStats.EatAndDrink(10, 0);

        Destroy(gameObject);
    }

}
