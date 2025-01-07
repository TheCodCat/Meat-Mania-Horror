using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class CutScenesController : MonoBehaviour
{
    [SerializeField] private List<PlayableDirector> playbles = new List<PlayableDirector>();
    private PlayableDirector _currentDirector;
    private int _index;

    private void Start()
    {
        _currentDirector = playbles[0];
    }
    public void Skip(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed) return;

        if(_index < playbles.Count - 1)
        {
            _currentDirector.Stop();
            Debug.Log(_currentDirector.state);
            _index += 1;
            _currentDirector = playbles[_index];
            Debug.Log(_currentDirector.state);
            _currentDirector.Play();
            Debug.Log(_currentDirector.state);
        }
    }

    public void ChangeCutScene(PlayableDirector playable)
    {
        _currentDirector.Stop();
        _currentDirector = playable;
        _currentDirector.Play();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        _currentDirector.Stop();
        Cursor.lockState = CursorLockMode.Locked;
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
    }
}
