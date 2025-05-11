using System;
using UnityEngine;

public class PlayerRotationWithCam : MonoBehaviour
{
    [SerializeField] private float _sensX = 300f;
    [SerializeField] private float _sensY = 300f;
    [SerializeField] private Transform orientation;

    private float _xRotation;
    private float _yRotation;

    private CharacterMovement _movement;
    private void Start()
    {
        _movement = orientation.GetComponentInChildren<CharacterMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CamSetting();
    }

    private void CamSetting()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensY;

        _yRotation += mouseX;
        _xRotation = 15;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        
        Quaternion cam = Quaternion.Euler(_xRotation, _yRotation, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, cam, 0.1f);

        
        if (_movement._velocity != Vector3.zero)
        {
            Quaternion player = Quaternion.Euler(0f, _yRotation, 0f);
            orientation.rotation = Quaternion.Slerp(orientation.rotation, player, 0.02f);
        }
    }
}
