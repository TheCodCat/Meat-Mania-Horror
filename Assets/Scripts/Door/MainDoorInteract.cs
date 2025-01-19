using Zenject;

public class MainDoorInteract : DoorInteract
{
    public override void Interact()
    {
        if (!_playerController.IsMainKey) return;
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
}
