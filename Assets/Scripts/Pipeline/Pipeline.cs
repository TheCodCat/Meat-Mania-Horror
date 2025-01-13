using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class Pipeline : MonoBehaviour
{
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private bool _isPlayning;
    [SerializeField] private Transform _position;
    private CharacterController _characterController;

    [Inject]
    public void Construct(CharacterController characterController)
    {
        Debug.Log("Все окей");
        _characterController = characterController;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isPlayning && other.transform.TryGetComponent(out CharacterController component))
        {
            PlayableDirector.Play();
            _isPlayning = true;
        }
    }
    public void SetpositionPlayer()
    {
        _characterController.transform.position = _position.position;
    }
}
