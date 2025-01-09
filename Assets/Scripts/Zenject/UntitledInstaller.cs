using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField] PlayerInput PlayerInput;
    [Header("Mover")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private CharacterController characterController;
    [Header("LookAt")]
    [SerializeField] private Camera _head;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromInstance(characterController);
        Container.Bind<Transform>().FromInstance(_playerTransform);

        Container.Bind<Camera>().FromInstance(_head);
        Container.BindInstance(PlayerInput);
    }
}