using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHungerThirst : MonoBehaviour
{
    private static PlayerHungerThirst _instance = null;
    public static PlayerHungerThirst Instance => _instance;

	[Header("Hunger & Thirst Mechanics")]
	[SerializeField] private float _hungerBaseDegrade = 0.1F;
    [SerializeField] private float _thirstBaseDegrade = 0.3F;
	[SerializeField] private float _walkMultiplier = 1.5F;
    [SerializeField] private float _sprintMultiplier = 4.0F;

    private PlayerMovement playerMovement;

    // Actual meters.
    private float _hunger = 100.0F;
	private float _thirst = 100.0F;

	public float Hunger => _hunger;
	public float Thirst => _thirst;

	public event Action OnUpdate;


    private void Awake()
    {
        _instance = this;
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
	{
		float hunger = _hungerBaseDegrade * Time.deltaTime;
        float thirst = _thirstBaseDegrade * Time.deltaTime;
        if (playerMovement.IsSprinting)
        {
            hunger *= _sprintMultiplier;
            thirst *= _sprintMultiplier;
        }
        else if (playerMovement.IsWalking)
        {
            hunger *= _walkMultiplier;
            thirst *= _walkMultiplier;
        }

        // Apply hunger and thirst.
        _hunger -= hunger;
        _thirst -= thirst;
        if (_hunger < 0.0F) _hunger = 0.0F;
        if (_thirst < 0.0F) _thirst = 0.0F;

        OnUpdate?.Invoke();
    }

    public void EatAndDrink(float foodAmount, float beverageAmount)
    {
        _hunger += foodAmount;
        _thirst += beverageAmount;
        if (_hunger > 100.0F) _hunger = 100.0F;
        if (_thirst > 100.0F) _thirst = 100.0F;
    }
}
