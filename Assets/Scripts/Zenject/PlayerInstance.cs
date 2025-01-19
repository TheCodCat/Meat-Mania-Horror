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
    [SerializeField] private PlayerMover _playerMover;
    [Header("LookAt")]
    [SerializeField] private CinemachineVirtualCamera _head;
    [SerializeField] private Camera _camera;
    [SerializeField] private LoudnessToMicrophone _loudnessToMicrophone;
    [SerializeField] private PlayerController _playerController;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromInstance(characterController);
        Container.Bind<Transform>().FromInstance(_playerTransform);

        Container.BindInstance(_playerController);
        Container.BindInstance(_loudnessToMicrophone);
        Container.BindInstance(_playerMover);
        Container.BindInstance(_head);
        Container.BindInstance(PlayerInput);
        Container.BindInstance(_camera);
    }
}