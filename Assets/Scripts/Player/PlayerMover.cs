using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _player;
    private CharacterController _controller;
    private bool _isGround;
    private Vector2 _inputDirection;
    private Vector3 _moveDirecotion;

    private const float _gravity = -9.8f;
    private const float _defaultSpeed = 5f;
    private const float _sprintSpeed = 8f;

    [Inject]
    public void Construct(CharacterController controller, Transform Player)
    {
        Debug.Log("получилось получить компонент");
        _controller = controller;
        _player = Player;
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
        Debug.Log(callbackContext);

        if(callbackContext.started)
            _speed = _sprintSpeed;
        else if(callbackContext.canceled) _speed = _defaultSpeed;
    }
}
