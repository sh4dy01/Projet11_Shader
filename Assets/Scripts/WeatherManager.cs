using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private ParticleSystem _particleSystem;
    
    [Header("Time values")]
    [SerializeField] private float _minRainDuration;
    [SerializeField] private float _maxRainDuration;
    [SerializeField] private float _minRainInterval;
    [SerializeField] private float _maxRainInterval;
    
    [SerializeField] private Light _directionalLight;
    [SerializeField] private float _secondsPerDay = 60.0F;
    [SerializeField] private float _timer = 0.0F;

    private float _timeBeforeRain;
    private bool _isRaining;
    private float _rainDuration;

    private void Awake()
    {
        _directionalLight.useColorTemperature = true;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        float lightAngle = _timer / _secondsPerDay * 360.0F;
        _directionalLight.transform.rotation = Quaternion.Euler(lightAngle, 0.0F, 0.0F);
        _directionalLight.intensity = Mathf.Clamp(Mathf.Sin(lightAngle * Mathf.Deg2Rad) * 10.0F, 0.0F, 1.0F);
        _directionalLight.colorTemperature = Mathf.Sin(lightAngle * Mathf.Deg2Rad) * (6800 - 2400) + 2400;
        Rain();
    }

    private void Rain()
    {
        if (_isRaining)
        {
            if(_rainDuration <= 0)
            {
                _particleSystem.Stop();
                _timeBeforeRain = Random.Range(_minRainInterval,_maxRainInterval);
            }
            _rainDuration -= Time.deltaTime;
        }
        else
        {
            if(_timeBeforeRain <= 0)
            {
                _particleSystem.Play();
                _rainDuration = Random.Range(_minRainDuration,_maxRainDuration);
                foreach (var material in _materials) material.SetFloat("Smoothness", 5f);
            }
            _timeBeforeRain -= Time.deltaTime;
        }
    }
}
