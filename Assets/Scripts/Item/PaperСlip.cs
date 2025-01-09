using UnityEngine;

public class PaperСlip : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _parent;
    public void Interact()
    {
        _playerController.GivePaperClip();
        _parent.SetActive(false);
    }
}
