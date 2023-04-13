using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] private PlayerEntity playerEntity;
	[SerializeField] private PlayerHungerThirst _playerHungerThirst;

	[SerializeField] private Image _hungerBar;
	[SerializeField] private Image _thirstBar;
	[SerializeField] private Image _healthBar;

	[SerializeField] private Material _HealthMaterial;
	
	private void Awake()
	{
		_playerHungerThirst.OnUpdate += UpdateValues;
		playerEntity.OnHit += UpdateHealth;
	}

	private void OnDestroy()
	{
		_playerHungerThirst.OnUpdate -= UpdateValues;
		playerEntity.OnHit -= UpdateHealth;
	}

	private void UpdateValues()
	{
		_hungerBar.fillAmount = _playerHungerThirst.Hunger / 100.0F;
		_thirstBar.fillAmount = _playerHungerThirst.Thirst / 100.0F;
	}

	private void UpdateHealth()
	{
		_healthBar.fillAmount = (float)playerEntity.Health / (float)playerEntity.MaxHealth;
		_HealthMaterial.SetFloat("_Health", (float)playerEntity.Health / (float)playerEntity.MaxHealth);
	}
}
