using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class AudioPlayerMover : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    private Vector2 _inputDirection;
    private bool _isMove;

    public void GetDirection(InputAction.CallbackContext callbackContext)
    {
        _inputDirection = callbackContext.ReadValue<Vector2>();
        if (!_inputDirection.y.Equals(0) || !_inputDirection.x.Equals(0) && !_isMove)
        {
            _isMove = true;
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
        else
        {
            _isMove = false;
            _audioSource.Stop();
        }
    }
}
