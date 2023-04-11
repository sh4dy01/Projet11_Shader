using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycleManager : MonoBehaviour
{
    [SerializeField] private Light _directionalLight;
    [SerializeField] private float _secondsPerDay = 60.0F;

    [SerializeField] private float _timer = 0.0F;

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
    }
}
