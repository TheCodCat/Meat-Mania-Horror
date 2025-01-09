using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerChangeMap : MonoBehaviour
{
    public static PlayerChangeMap Instance;
    private PlayerInput PlayerInput;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else Destroy(Instance.gameObject);
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
            _ => "Game"
        };

        PlayerInput.SwitchCurrentActionMap(newstate);
        Debug.Log(PlayerInput.currentActionMap);
    }
}

