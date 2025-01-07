using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;

    private bool _isGround;
    private Vector2 _inputDirection;
    private Vector3 _moveDirecotion;

    private const float _gravity = -9.8f;
    private const float _defaultSpeed = 5f;
    private const float _sprintSpeed = 8f;

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _isGround = _controller.isGrounded;

        if (_isGround)
            _moveDirecotion.y = -1;
        else 
            _moveDirecotion.y += _gravity * Time.deltaTime;

        _controller.Move(transform.TransformDirection(_moveDirecotion) * Time.deltaTime * _speed);
    }

    public void GetInputDirection(InputAction.CallbackContext callbackContext)
    {
        _inputDirection = callbackContext.ReadValue<Vector2>();

        _moveDirecotion.x = _inputDirection.x;
        _moveDirecotion.z = _inputDirection.y;
    }

    public void Sprint(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(callbackContext);

        if(callbackContext.started)
            _speed = _sprintSpeed;
        else if(callbackContext.canceled) _speed = _defaultSpeed;
    }
}
