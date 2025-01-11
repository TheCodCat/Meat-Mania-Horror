using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;


public class LockerController : MonoBehaviour, IInteractable
{
    public bool IsLocker { get; private set; }
    [SerializeField] private PlayableDirector _escape;
    [SerializeField] private PlayableDirector _exit;
    [SerializeField] private Transform _enterPoint;
    [SerializeField] private Transform _insidePoint;
    [SerializeField] private CinemachineBrain _brain;
    private Transform _player;
    private CinemachineVirtualCamera _head;

    [Inject]
    public void Construct(CharacterController characterController, CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        _player = characterController.transform;
        _head = cinemachineVirtualCamera;
    }
    public void Interact()
    {
        if (!IsLocker)
            _escape.Play();
        else
            _exit.Play();
    }

    public void SetStateInside()
    {
        PlayerChangeMap.Instance.ChangeState(PlayerState.Inside);
    }

    public void SetStateGame()
    {
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
    }

    public void SetPositionEnter()
    {
        _player.transform.position = _enterPoint.position;
    }

    public void SetCamera(CinemachineVirtualCamera cinemachineVirtualCamera)
    {
        _brain.SetCameraOverride(0,_head,cinemachineVirtualCamera, 2f,2f);
    }

    public void SetPositionInsate()
    {
        _player.transform.position = _insidePoint.position;
    }

    public void SetRotationCamera(Transform transform)
    {
        _head.transform.rotation = transform.rotation;
    }

    public void SetStateLocker(bool inside)
    {
        IsLocker = inside;
    }
}
