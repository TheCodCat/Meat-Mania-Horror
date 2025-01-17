using UnityEngine;
using Zenject;

public class PaperСlip : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _parent;

    private SaveController _saveController;
    [Inject]
    public void Construct(SaveController saveController)
    {
        _saveController = saveController;
    }
    public void Interact()
    {
        _saveController.PlayerData.IsGrapKey = true;
        _playerController.GivePaperClip();
        _parent.SetActive(false);
    }
}
