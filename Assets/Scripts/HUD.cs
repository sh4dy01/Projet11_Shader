using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField] private PlayerHungerThirst _playerHungerThirst;

	//[SerializeField] private Image _hungerBar;
	//[SerializeField] private Image _thirstBar;

	//private Vector2 _baseSize;

	private void Awake()
	{
		//_baseSize = _hungerBar.rectTransform.sizeDelta;
		_playerHungerThirst.OnUpdate += UpdateValues;
	}

	private void OnDestroy()
	{
		_playerHungerThirst.OnUpdate -= UpdateValues;
	}

	public void UpdateValues()
	{
		//_hungerBar.rectTransform.sizeDelta = _baseSize * new Vector2(_playerHungerThirst.Hunger / 100.0F, 1.0F);
		//_thirstBar.rectTransform.sizeDelta = _baseSize * new Vector2(_playerHungerThirst.Thirst / 100.0F, 1.0F);
	}
}
