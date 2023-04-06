using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] private PlayerHungerThirst _playerHungerThirst;

	[SerializeField] private Image _hungerBar;
	[SerializeField] private Image _thirstBar;


	private void Awake()
	{
		_playerHungerThirst.OnUpdate += UpdateValues;
	}

	private void OnDestroy()
	{
		_playerHungerThirst.OnUpdate -= UpdateValues;
	}

	public void UpdateValues()
	{
		_hungerBar.fillAmount = _playerHungerThirst.Hunger / 100.0F;
		_thirstBar.fillAmount = _playerHungerThirst.Thirst / 100.0F;
	}
}
