using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private Material[] _materials;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField][Range(0,1)] private float _reflectionIntensity;
    
    [Header("Rain")]
    [SerializeField] private float _minRainDuration;
    [SerializeField] private float _maxRainDuration;
    [SerializeField] private float _minRainInterval;
    [SerializeField] private float _maxRainInterval;
    [SerializeField][Range(0,1)] private float _ripplesStength = 0.3f;
    [SerializeField][Range(0,1)] private float _reflectionStength = 1;
    
    [Header("Fades")]
    [SerializeField] private float _fadeInReflexionDuration;
    [SerializeField] private float _fadeOutReflexionDuration;
    [SerializeField] private float _fadeInCloudsDuration;   
    [SerializeField] private float _fadeOutCloudsDuration;

    
    [SerializeField] private Light _directionalLight;
    [SerializeField] private float _secondsPerDay = 60.0F;
    [SerializeField] private float _timer = 0.0F;

    private float _rainTimer;
    private float _fadeReflexionCount;
    private float _fadeCloudsCount;
    private bool _isRaining;

    private void Awake()
    {
        _directionalLight.useColorTemperature = true;
        _rainTimer = 10;
        _fadeReflexionCount = 0;
        _fadeCloudsCount = 0;
        _isRaining = false;
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
            if (_fadeReflexionCount >= 0)
                foreach (var material in _materials)
                {
                    material.SetFloat("_Smoothness", (_fadeReflexionCount/_fadeInReflexionDuration)*_reflectionStength);
                    material.SetFloat("_RippleStrength", _ripplesStength-((_fadeReflexionCount/_fadeInReflexionDuration)*_ripplesStength));
                }

            if (_fadeCloudsCount >= 0)
            {
                float value = Mathf.Clamp(_fadeCloudsCount / _fadeInCloudsDuration,0.5f,1f);
                _directionalLight.color = new Color(value,value,value,1);
            }

            if (_rainTimer <= 0)
            {
                _particleSystem.Stop();
                
                _rainTimer = Random.Range(_minRainInterval, _maxRainInterval);
                
                _isRaining = false;
                _fadeReflexionCount = _fadeOutReflexionDuration;
                _fadeCloudsCount = _fadeOutCloudsDuration;
            }

        }
        else
        {
            if (_fadeReflexionCount >= 0)
                foreach (var material in _materials)
                {
                    material.SetFloat("_Smoothness", 1-((_fadeReflexionCount/_fadeOutReflexionDuration)*_reflectionStength));
                }

            if (_fadeCloudsCount >= 0)
            {
                foreach (var material in _materials)
                {
                    material.SetFloat("_RippleStrength", _ripplesStength * _fadeReflexionCount / _fadeInReflexionDuration);
                }
                float value = Mathf.Clamp(1-_fadeCloudsCount / _fadeOutCloudsDuration,0.5f,1f);
                _directionalLight.color = new Color(value,value,value,1);
            }
            
            if (_rainTimer <= 0)
            {
                _particleSystem.Play();
                
                _rainTimer = Random.Range(_minRainDuration,_maxRainDuration);
                
                _isRaining = true;
                _fadeReflexionCount = _fadeInReflexionDuration;
                _fadeCloudsCount = _fadeInCloudsDuration;
            }
            
        }
        _rainTimer -= Time.deltaTime;
        _fadeReflexionCount -= Time.deltaTime;
        _fadeCloudsCount -= Time.deltaTime;

    }
}
