using System;
using Cinemachine;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _zoomSpeed = 1f;
    [SerializeField] [Range(2, 6)] private float _minFOV = 4;
    [SerializeField] [Range(6, 14)] private float _maxFOV = 10;
    
    private float _pitch;
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        _pitch = transform.eulerAngles.y;
    }

    private void Update()
    {
        RotateCamera();
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(_virtualCamera.m_Lens.OrthographicSize - Input.mouseScrollDelta.y * _zoomSpeed, _minFOV, _maxFOV);
    }

    private void RotateCamera()
    {
        if (!Input.GetMouseButton(2)) return;

        _pitch -= _rotationSpeed * Input.GetAxis("Mouse X");

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _pitch, transform.eulerAngles.z);
    }
}
