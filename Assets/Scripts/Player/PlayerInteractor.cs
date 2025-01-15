using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _distance;
    private Transform _camera;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _spriteNotInteract;
    [SerializeField] private Sprite _spriteInteract;
    [SerializeField] private GameObject _light;
    [SerializeField] private bool _lightEnable;

    [Inject]
    public void Construct(CinemachineVirtualCamera camera)
    {
        _camera = camera.transform;
    }

    public void Interact(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) return;

        Ray ray = new Ray(_camera.position,_camera.forward);

        Debug.DrawRay(ray.origin,ray.direction * _distance, Color.blue);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if (hit.transform.TryGetComponent(out IInteractable component))
            {
                component.Interact();
            }
        }
    }

    private void LateUpdate()
    {
        Ray ray = new Ray(_camera.position, _camera.forward);

        Debug.DrawRay(ray.origin, ray.direction * _distance, Color.blue);

        if (Physics.Raycast(ray, out RaycastHit hit, _distance))
        {
            if (hit.transform.TryGetComponent(out IInteractable component))
            {
                _image.sprite = _spriteInteract;
            }
            else _image.sprite = _spriteNotInteract;
        }
        else _image.sprite = _spriteNotInteract;
    }

    public void ActiveLight(InputAction.CallbackContext callbackContext)
    {
        _lightEnable = !_lightEnable;
        _light.SetActive(_lightEnable);
    }
}
