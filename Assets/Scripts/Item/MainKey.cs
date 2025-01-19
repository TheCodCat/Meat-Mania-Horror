using UnityEngine;

public class MainKey : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _parent;
    public void Interact()
    {
        _playerController.GiveKeyClip();
        _parent.SetActive(false);
    }
}
