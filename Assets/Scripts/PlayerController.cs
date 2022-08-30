using System;
using Cinemachine;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private Joystick joystick;
    public float speed = 2f;
    
    private float _curentMoveMultiplier;
    [SerializeField] private float _acceleration;
  
    [SerializeField]public float turnSpeed;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var inputVector = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        var cameraTransform = _camera.transform;
        var forward = cameraTransform.forward;
        var cameraForwardHorizontal =
            new Vector3(forward.x, 0f, forward.z).normalized;

        var right = cameraTransform.right;
        var cameraRightHorizontal =
            new Vector3(right.x, 0f, right.z).normalized;

        var movementVector = inputVector.x * cameraRightHorizontal + inputVector.z * cameraForwardHorizontal;

        var dot = Mathf.Clamp(Vector3.Dot(transform.forward, inputVector),0,1);

        _curentMoveMultiplier = Mathf.Lerp(_curentMoveMultiplier, dot, _acceleration * Time.fixedDeltaTime);
        
        var position = transform.position + transform.forward.normalized * (speed * _curentMoveMultiplier * Time.fixedDeltaTime);

        transform.position = position;

        if (inputVector.magnitude > 0)
        {
            var newRotation = Quaternion.LookRotation(movementVector, Vector3.up);
        
            var rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.fixedDeltaTime);
            
            transform.rotation = rotation;
        }
    }
    
}
