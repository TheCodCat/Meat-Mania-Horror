using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInstance : MonoInstaller
{
    [SerializeField] PlayerInput PlayerInput;
    [Header("Mover")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private CharacterController characterController;
    [Header("LookAt")]
    [SerializeField] private CinemachineVirtualCamera _head;
    [SerializeField] private SaveController _saveController;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromInstance(characterController);
        Container.Bind<Transform>().FromInstance(_playerTransform);

        Container.BindInstance(_head);
        Container.BindInstance(PlayerInput);
        Container.BindInstance(_saveController);
    }
}