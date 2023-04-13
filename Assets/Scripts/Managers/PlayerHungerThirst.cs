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
    [SerializeField] private float _loseHealthCooldown = 10F;

    private PlayerMovement _playerMovement;
    private PlayerEntity _playerEntity;
    private float _loseHealthTimer = 0.0F;

    // Actual meters.
    [SerializeField] private float _hunger = 100.0F;
    [SerializeField] private float _thirst = 100.0F;

	public float Hunger => _hunger;
	public float Thirst => _thirst;

	public event Action OnUpdate;


    private void Awake()
    {
        _instance = this;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerEntity = GetComponent<PlayerEntity>();
        OnUpdate?.Invoke();
    }

    private void Update()
	{
		float hunger = _hungerBaseDegrade * Time.deltaTime;
        float thirst = _thirstBaseDegrade * Time.deltaTime;
        if (_playerMovement.IsSprinting)
        {
            hunger *= _sprintMultiplier;
            thirst *= _sprintMultiplier;
        }
        else if (_playerMovement.IsWalking)
        {
            hunger *= _walkMultiplier;
            thirst *= _walkMultiplier;
        }

        // Apply hunger and thirst.
        _hunger -= hunger;
        _thirst -= thirst;
        if (_hunger < 0.0F) _hunger = 0.0F;
        if (_thirst < 0.0F) _thirst = 0.0F;

        if (_hunger <= 0.0F && _thirst <= 0.0F)
        {
            _loseHealthTimer -= Time.deltaTime;

            if (_loseHealthTimer <= 0)
            {
                _playerEntity.TakeDamage(1);
                _loseHealthTimer = _loseHealthCooldown;
            }
        }

        OnUpdate?.Invoke();
    }

    public void EatAndDrink(float foodAmount, float beverageAmount)
    {
        _hunger += foodAmount;
        _thirst += beverageAmount;
        if (_hunger > 100.0F) _hunger = 100.0F;
        if (_thirst > 100.0F) _thirst = 100.0F;
    }
    
    public void DecreaseHungerOnHealth(float hungerAmount)
    {
        _hunger -= hungerAmount;
        if (_hunger < 0.0F) _hunger = 0.0F;
        OnUpdate?.Invoke();
    }
}
