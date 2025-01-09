using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using Assets.Scripts.Models;

public class SaveController : MonoBehaviour
{
    public PlayableDirector CurrentPlayableDirector { get;set; }
    private IDrawning Component;

    private CancellationTokenSource CancellationTokenSource;
    private bool _drawning = false;
    private Camera _camera;
    Vector2 _mousePosition;

    [Inject]
    public void Construct(Camera camera)
    {
        _camera = camera;
    }

    /// <summary>
    /// Начало сохранения, принимает директора
    /// </summary>
    /// <param name="playableDirector"></param>
    /// 
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
        CurrentPlayableDirector = null;
        Component?.Init();
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
        if (callbackContext.performed)
        {
            Debug.Log(_drawning);

            while (_drawning)
            {
                Ray ray = _camera.ScreenPointToRay(_mousePosition);
                Debug.DrawRay(ray.origin,ray.direction,Color.red, 10f);

                if(Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if(hitInfo.transform.TryGetComponent(out Component))
                    {
                        Component.Draw(hitInfo.textureCoord);
                    }
                }

                await UniTask.Yield();
            }
        }
        else if(callbackContext.canceled)
            ExitSaves();
    }

    public void GetMousePosition(InputAction.CallbackContext callbackContext) 
    {
        _mousePosition = callbackContext.ReadValue<Vector2>();
    }

    public void SaveGud()
    {
        ExitSaves();
        Debug.Log("Сохранение прошло");
    }
}
