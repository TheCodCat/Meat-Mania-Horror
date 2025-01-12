using UnityEngine;
using UnityEngine.InputSystem;

public class DoorManader : MonoBehaviour
{
    [SerializeField] private float _rotateMoment;
    [SerializeField] private float _inputRotateObject;
    [SerializeField] protected float _openRotate;

    [SerializeField] private Vector3 _rotation;
    [SerializeField] private Vector3 _startrotation;

    [SerializeField] private Transform _transform;
    [SerializeField] private Animator _animator;

    [SerializeField] private VectorRotate _vectorRotate;

    private void Start()
    {
        _startrotation = _transform.localEulerAngles;
    }
    public void GetRotation(InputAction.CallbackContext callbackContext)
    {
        _inputRotateObject += callbackContext.ReadValue<Vector2>().x * _rotateMoment;

        _inputRotateObject %= 360;

        _rotation = _vectorRotate switch
        {
            VectorRotate.X => new Vector3(_inputRotateObject,0,0),
            VectorRotate.Y => new Vector3(0, _inputRotateObject, 0),
            VectorRotate.Z => new Vector3(0,0,_inputRotateObject),
            _ => new Vector3(_inputRotateObject, 0, 0)
        };
        _rotation += _startrotation;

        _transform.localEulerAngles = _rotation;

        if(_transform.eulerAngles.y <= _openRotate + 10 && _transform.localEulerAngles.y >= -_openRotate - 10)
        {
            Debug.Log("Дверь открыта");
            _animator.SetTrigger("Open");
        }

    }

    public enum VectorRotate
    {
        X, Y, Z
    }
}
