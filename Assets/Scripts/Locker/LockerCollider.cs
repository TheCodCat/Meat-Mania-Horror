using UnityEngine;
using UnityEngine.Playables;

public class LockerCollider : MonoBehaviour, IInteractable
{
    [SerializeField] private LockerController _controller;

    //[SerializeField] Animator _animator;
    //public const string OPEN_KEY = "Open";

    public void Interact()
    {
        _controller.Interact();
        //_animator.SetTrigger(OPEN_KEY);
    }
}
