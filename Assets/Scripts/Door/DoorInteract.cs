using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public abstract class DoorInteract : MonoBehaviour, IInteractable
{
    public Vector3 Rotation;
    public Transform Transform;
    public Vector3 Startrotation;
    public Animator Animator;
    [SerializeField] private PlayableDirector _playableDirectorEnter;
    [SerializeField] private PlayableDirector _playableDirectorExit;
    [SerializeField] protected bool _isOpenKey;
    [SerializeField] protected bool _isOpen;
    protected PlayerController _playerController;

    [Inject]
    public void Construct(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void InitSave(bool isopen)
    {
        _isOpenKey = isopen;
    }

    public virtual void Interact()
    {
        if(!_playerController.IsPaperGiv) return;
        if (!_isOpenKey)
        {
            Init();
            PlayerChangeMap.Instance.ChangeState(PlayerState.BreakingLock);
        }
        else
        {
            _isOpen = !_isOpen;
            if (_isOpen)
                Animator.SetTrigger("Open");
            else
                Animator.SetTrigger("Close");
        }
    }
    public void Init()
    {
        Startrotation = Transform.localEulerAngles;
        DoorManader.instance.SetOpenDoor(this);
        DoorManader.instance.IsOpenCurrentDoor = false;
        _playableDirectorEnter.Play();
    }
    public void DoorOpenAnimation(string log = "Нен")
    {
        Debug.Log(log);
        Animator.SetTrigger("Open");
        _isOpen = true;
        _isOpenKey = true;
        _playableDirectorExit.Play();
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
        DoorManader.instance.SetOpenDoor(null);
    }

    public void ExitPlayebleDirector(PlayableDirector playableDirector)
    {
        playableDirector.Stop();
    }

    public DoorInteract GetDoor()
    {
        return this;
    }

    public void ExitDoor()
    {
        _playableDirectorExit.Play();
    }

    public bool GetOpen()
    {
        return _isOpenKey;
    }
}
