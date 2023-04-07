using UnityEngine;

public class CollectConsumables : CollectibleItem
{
    [SerializeField] private int _foodAmount = 25;
    [SerializeField] private int _waterAmount = 10;
    
    protected override void Collect(GameObject player)
    {
        player.GetComponent<PlayerHungerThirst>().EatAndDrink(_foodAmount, _waterAmount);
    }

}
