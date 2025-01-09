using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField] PlayerInput PlayerInput;
    [Header("Mover")]
    [SerializeField] private Transform _playerTransform;
    [Header("LookAt")]
    [SerializeField] private CinemachineVirtualCamera _head;
    public override void InstallBindings()
    {
        Container.Bind<Transform>().FromInstance(_playerTransform);

        Container.BindInstance(_head);
        Container.BindInstance(PlayerInput);
    }
}