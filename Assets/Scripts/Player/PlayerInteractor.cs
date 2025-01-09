using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float _distance;
    private Transform _camera;

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
}
