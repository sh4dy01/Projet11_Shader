using System;
using UnityEngine;

public class CollectWater : CollectibleItem
{
    [SerializeField] private PlayerHungerThirst playerStats;
    
    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        playerStats.EatAndDrink(0, 10);

        Destroy(gameObject);
    }

}
