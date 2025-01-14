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
    [SerializeField] private SaveController _saveController;
    [SerializeField] private LoudnessToMicrophone _loudnessToMicrophone;
    public override void InstallBindings()
    {
        Container.Bind<CharacterController>().FromInstance(characterController);
        Container.Bind<Transform>().FromInstance(_playerTransform);

        Container.BindInstance(_loudnessToMicrophone);
        Container.BindInstance(_playerMover);
        Container.BindInstance(_head);
        Container.BindInstance(PlayerInput);
        Container.BindInstance(_saveController);
        Container.BindInstance(_camera);
    }
}