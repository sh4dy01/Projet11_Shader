using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{
    [SerializeField] private PlayerEntity _player;
    [SerializeField] [Range(1, 5)] private int _lowHpThreshold;

    private Volume _postProcessVolume;
    private Vignette _vignette;
    
    private float _vignetteIntensity = 0.0F;
    private float _vignetteIntensityInertia = 0.0F;

    private readonly float _targetVignetteIntensity = 0.5F;

    private void Awake()
    {
        _postProcessVolume = GetComponent<Volume>();
        _postProcessVolume.profile.TryGet(out _vignette);
    }

    private void Update()
    {
        bool hurt = _player.Health <= _lowHpThreshold;

        if (hurt)
        {
            _vignetteIntensityInertia += (_targetVignetteIntensity - _vignetteIntensity) * 4.0F * Time.deltaTime;
            if (_vignetteIntensityInertia > 0.2F) _vignetteIntensityInertia = 0.2F;
            _vignetteIntensity += _vignetteIntensityInertia * Time.deltaTime;
        }
        else
        {
            _vignetteIntensity -= Time.deltaTime * 0.3F;
            if (_vignetteIntensity < 0.0F) _vignetteIntensity = 0.0F;
        }

        _vignette.intensity.value = _vignetteIntensity;
    }
}
