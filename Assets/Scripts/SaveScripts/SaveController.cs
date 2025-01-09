using Assets.Scripts.Models;
using Assets.Scripts.SaveScripts;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;

public class SaveController : MonoBehaviour
{
    private PlayableDirector CurrentPlayableDirector;
    private StateMashine StateMashine = new StateMashine();
    public StartSave StartSave { get; private set; } = new StartSave();
    public void StartSaves(PlayableDirector playableDirector)
    {
        CurrentPlayableDirector = playableDirector;
        PlayerChangeMap.Instance.ChangeState(PlayerState.Save);
        StateMashine.ChangeState(StartSave);
        CurrentPlayableDirector.Play();
    }
    public void ExitSaves()
    {
        Debug.Log("Выходим из сохранения");
        CurrentPlayableDirector.Stop();
        PlayerChangeMap.Instance.ChangeState(PlayerState.Game);
        CurrentPlayableDirector = null;
    }
    public void ExitSaves(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            ExitSaves();
        }
    }
}
