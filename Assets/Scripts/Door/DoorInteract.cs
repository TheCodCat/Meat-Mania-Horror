using UnityEngine;
using UnityEngine.Playables;

public class DoorInteract : MonoBehaviour, IInteractable
{
    public Vector3 Rotation;
    public Transform Transform;
    public Vector3 Startrotation;
    public Animator Animator;
    [SerializeField] private PlayableDirector _playableDirectorEnter;
    [SerializeField] private PlayableDirector _playableDirectorExit;
    [SerializeField] private bool _isOpenKey;
    [SerializeField] private bool _isOpen;
    public void Interact()
    {
        Debug.Log(gameObject.name);
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
        _playableDirectorEnter.Play();
    }
    public void DoorOpenAnimation()
    {
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
}
