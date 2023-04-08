using System;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] [Range(2, 6)] private float _minFOV = 4;
    [SerializeField] [Range(6, 14)] private float _maxFOV = 10;
    
    private CinemachineVirtualCamera _virtualCamera;
    private LensSettings _lensSettings;
    private float _rotationY;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _rotationY = transform.eulerAngles.y;
    }

    private void Update()
    {
        RotateCamera();
        ZoomCamera();
    }

    private void ZoomCamera()
    {
        if (Input.mouseScrollDelta.y == 0) return;
            
            _virtualCamera.m_Lens.OrthographicSize =
                Mathf.Clamp(_virtualCamera.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * _zoomSpeed, _minFOV, _maxFOV);
    }

    private void RotateCamera()
    {
        if (!Input.GetMouseButton(2)) return;

        _rotationY -= _rotationSpeed * Input.GetAxis("Mouse X");

        var eulerAngles = transform.eulerAngles;
        eulerAngles = new Vector3(eulerAngles.x, _rotationY, eulerAngles.z);
        transform.eulerAngles = eulerAngles;
    }
}
