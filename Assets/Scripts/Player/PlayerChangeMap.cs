using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerChangeMap : MonoBehaviour
{
    public static PlayerChangeMap Instance;
    private PlayerInput PlayerInput;
    private LoadSceneManager LoadSceneManager;
    private bool _isMenu;
    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else Destroy(Instance.gameObject);
    }

    private void Start()
    {
        ChangeState(PlayerState.Game);
    }

    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        Debug.Log("Все ок");
        PlayerInput = playerInput;
    }

    public void ChangeState(PlayerState playerState)
    {
        string newstate = playerState switch
        {
            PlayerState.CutScene => "Timeline",
            PlayerState.Game => "Game",
            PlayerState.Save => "Save",
            PlayerState.Pause => "Pause",
            PlayerState.Inside => "Inside",
            PlayerState.BreakingLock => "BreakingLock",
            _ => "Pause"
        };

        switch (playerState)
        {
            case PlayerState.Game:
                Cursor.lockState = CursorLockMode.Locked;
                break;
            default:
                Cursor.lockState = CursorLockMode.None;
                break;
        }

        PlayerInput.SwitchCurrentActionMap(newstate);
        //Debug.Log(PlayerInput.currentActionMap);
    }

    public void PauseMenu(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            _isMenu = !_isMenu;
            var operation = _isMenu == true ? PlayerState.Pause : PlayerState.Game;
            _pauseMenu.SetActive(_isMenu);
            ChangeState(operation);
        }
    }
}

