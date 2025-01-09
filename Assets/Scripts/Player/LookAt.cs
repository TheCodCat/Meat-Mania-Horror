using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LookAt : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float _sensetivity;
    private Transform _body;
    private CinemachineVirtualCamera _camera;

    private Vector2 _rotation;

    private const float MinX = -80;
    private const float MaxX = 80;

    [Inject]
    public void Construct(Transform body, CinemachineVirtualCamera head)
    {
        Debug.Log("Все ок");
        _body = body;
        _camera = head;
    }

    public void Look(InputAction.CallbackContext callbackContext)
    {
        _rotation.x -= callbackContext.ReadValue<Vector2>().y * _sensetivity;
        _rotation.y += callbackContext.ReadValue<Vector2>().x * _sensetivity;

        _rotation.x = Mathf.Clamp(_rotation.x, MinX,MaxX);

        _body.localRotation = Quaternion.Euler(0,_rotation.y,0);
        _camera.transform.localRotation = Quaternion.Euler(_rotation.x,0,0);
    }

    public void Init()
    {
        _rotation.x = _camera.transform.rotation.y;
        _rotation.y = _body.rotation.x;
    }
}
