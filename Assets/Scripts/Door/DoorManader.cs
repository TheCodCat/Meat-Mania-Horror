using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class DoorManader : MonoBehaviour
{
    public static DoorManader instance;
    [SerializeField] private float _rotateMoment;
    [SerializeField] private float _inputRotateObject;
    [SerializeField] private float _openRotate;
    [SerializeField] private VectorRotate _vectorRotate;
    [SerializeField] private DoorInteract _currentDoor;
    public bool IsOpenCurrentDoor;

    private void Awake()
    {
        instance = this;
    }
    public void GetRotation(InputAction.CallbackContext callbackContext)
    {
        _inputRotateObject += callbackContext.ReadValue<Vector2>().x * _rotateMoment;

        _inputRotateObject %= 360;

        _currentDoor.Rotation = _vectorRotate switch
        {
            VectorRotate.X => new Vector3(_inputRotateObject,0,0),
            VectorRotate.Y => new Vector3(0, _inputRotateObject, 0),
            VectorRotate.Z => new Vector3(0,0,_inputRotateObject),
            _ => new Vector3(_inputRotateObject, 0, 0)
        };
        _currentDoor.Rotation += _currentDoor.Startrotation;

        _currentDoor.Transform.localEulerAngles = _currentDoor.Rotation;

        _currentDoor.Rotation.x %= 360;
        _currentDoor.Rotation.y %= 360;
        _currentDoor.Rotation.z %= 360;

        float currentratio = _vectorRotate switch
        {
            VectorRotate.X => _currentDoor.Rotation.x,
            VectorRotate.Y => _currentDoor.Rotation.y,
            VectorRotate.Z => _currentDoor.Rotation.z,
            _ => 0
        };
        if(callbackContext.performed && !IsOpenCurrentDoor)
        {
            if(currentratio <= _openRotate + 5 && currentratio >= _openRotate - 5)
            {
                try
                {
                    IsOpenCurrentDoor = true;
                    _currentDoor.DoorOpenAnimation(callbackContext.phase.ToString());
                    _inputRotateObject = 0;
                }
                catch(Exception ex)
                {
                    Debug.Log(gameObject.name);
                }
            }
        }
    }

    public enum VectorRotate
    {
        X, Y, Z
    }

    public void ExitDoor(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            ExitCurrentDoor();
        }
    }

    public void ExitCurrentDoor()
    {
        _currentDoor.ExitDoor();
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
    }
    public void SetOpenDoor(DoorInteract doorInteract)
    {
        _currentDoor = doorInteract;
    }
}
