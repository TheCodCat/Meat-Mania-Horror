using UnityEngine;

public class LockerCollider : MonoBehaviour, IInteractable
{
    [SerializeField] Animator _animator;
    public const string OPEN_KEY = "Open";

    public void Interact()
    {
        _animator.SetTrigger(OPEN_KEY);
    }
}
