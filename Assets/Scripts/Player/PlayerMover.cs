using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField, Range(0, 1.7f)] private float _minHeight;
    private Vector3 _currectHeight;
    private Transform _player;
    private CharacterController _controller;
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private bool _isGround;
    private Vector2 _inputDirection;
    private Vector3 _moveDirecotion;

    private const float _gravity = -9.8f;

    private const float _defaultSpeed = 5f;
    private const float _sprintSpeed = 6f;

    private const float _defaultSpeedOach = 3f;
    private const float _sprintSpeedOach = 4f;
    private bool _isOach;
    private const float _maxHeight = 1.8f;
    [Inject]
    public void Construct(CharacterController controller, Transform Player, CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        Debug.Log("получилось получить компонент");
        _controller = controller;
        _player = Player;
        _cinemachineVirtualCamera = cinemachineVirtualCamera;
    }

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
        if (callbackContext.performed && _isOach)
            _speed = _sprintSpeedOach;
        else if(callbackContext.performed && !_isOach)
            _speed = _sprintSpeed;

        if (callbackContext.canceled && _isOach)
            _speed = _defaultSpeedOach;

        else if(callbackContext.canceled && !_isOach)
            _speed = _defaultSpeed;
    }

    public void Oath(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _isOach = !_isOach;

            if (_isOach)
            {
                _speed = _defaultSpeedOach;
                _currectHeight.y = _minHeight;
            }
            else
            {
                _speed = _defaultSpeed;
                _currectHeight.y = _maxHeight;
            }
            _cinemachineVirtualCamera.transform.localPosition = _currectHeight;
            _controller.height = _currectHeight.y;
            _controller.center = _currectHeight / 2;
        }
    }
}
