using UnityEngine;
using Zenject;

public class MainDoorInteract : DoorInteract
{
    private CharacterController _characterController;
    [Inject]
    public void Construct(CharacterController characterController)
    {
        _characterController = characterController;
    }
    public override void Interact()
    {
        if (!_playerController.IsMainKey) return;

        Init();
        PlayerChangeMap.Instance.ChangeState(PlayerState.BreakingLock);
    }

    public void SetPositionPlayer(Transform transform)
    {
        _characterController.transform.position = transform.position;
    }
}
