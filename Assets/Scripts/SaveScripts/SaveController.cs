using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using System.Threading;
using Cysharp.Threading.Tasks;

public class SaveController : MonoBehaviour
{
    public PlayableDirector CurrentPlayableDirector { get;set; }

    private CancellationTokenSource CancellationTokenSource;
    private bool _drawning = false;

    /// <summary>
    /// Начало сохранения, принимает директора
    /// </summary>
    /// <param name="playableDirector"></param>
    public void StartSaves(PlayableDirector playableDirector)
    {
        CurrentPlayableDirector = playableDirector;
        PlayerChangeMap.Instance.ChangeState(PlayerState.Save);
        CurrentPlayableDirector.Play();
    }

    public void ExitSaves()
    {
        Debug.Log("Выходим из сохранения");
        CurrentPlayableDirector?.Stop();
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
        //CurrentPlayableDirector = null;
    }

    public void ExitSaves(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            ExitSaves();
        }
    }

    public async void Drawning(InputAction.CallbackContext callbackContext)
    {
        _drawning = callbackContext.ReadValueAsButton();
        Debug.Log(_drawning);

        while (_drawning)
        {
            Debug.Log("Я рисую");
            await UniTask.Yield();
        }

        ExitSaves();
    }
}
