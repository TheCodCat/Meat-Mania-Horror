using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.InputSystem;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using Assets.Scripts.Models;
using Assets.Scripts.SaveScripts;
using System.IO;

public class SaveController : MonoBehaviour
{
    public PlayableDirector CurrentPlayableDirector { get;set; }
    private IDrawning Component;

    private CancellationTokenSource CancellationTokenSource;
    private bool _drawning = false;
    private Camera _camera;
    Vector2 _mousePosition;

    RaycastHit _raycastHit;
    Ray _ray;

    private CharacterController _characterController;
    [SerializeField] private string _fileName;
    [SerializeField] private string _dataPathData;
    [SerializeField] private PlayerData _playerData;
    private void Awake()
    {
        _dataPathData = Path.Combine(Application.persistentDataPath,_fileName);

        if (File.Exists(_dataPathData))
        {
            _playerData = DataSaver.Deserializable<PlayerData>(_dataPathData);

            Vector3 startPosition = new Vector3(_playerData.Position.X, _playerData.Position.Y, _playerData.Position.Z);
            _characterController.transform.position = startPosition;
        }
    }

    [Inject]
    public void Construct(Camera camera, CharacterController characterController)
    {
        _camera = camera;
        _characterController = characterController;
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
        Component.Exit();
        Component?.Init();
        Debug.Log("Выходим из сохранения");
        CurrentPlayableDirector?.Stop();
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

    public async void Drawning(InputAction.CallbackContext callbackContext)
    {
        _drawning = callbackContext.ReadValueAsButton();

        _ray = _camera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Debug.DrawRay(_ray.origin, _ray.direction, Color.red, 10f);

        if (Physics.Raycast(_ray, out _raycastHit))
        {
            if (_raycastHit.transform.TryGetComponent(out IDrawning component))
            {
                Component = component;
            }
        }

        if (callbackContext.performed)
        {
            Debug.Log(_drawning);

            while (_drawning)
            {
                _ray = _camera.ScreenPointToRay(_mousePosition);
                if (Physics.Raycast(_ray, out _raycastHit))
                {
                    if(_raycastHit.transform.TryGetComponent(out IDrawning Component))
                    {
                        Component.Draw(_raycastHit.textureCoord);
                    }
                    else ExitSaves();
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

    public void SaveGud(Vector3 vector3)
    {
        PlayerData playerData = new PlayerData();
        playerData.Position = new MyVector3(vector3.x, vector3.y, vector3.z);
        _playerData.Position = playerData.Position;
        DataSaver.Serializable(_dataPathData, _playerData);


        ExitSaves();
        Debug.Log("Сохранение прошло");
    }
}
