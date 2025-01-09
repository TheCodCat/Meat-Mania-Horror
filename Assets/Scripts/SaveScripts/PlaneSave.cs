using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class SaveTest : MonoBehaviour, IInteractable
{
    private SaveController _controller;
    [SerializeField] private PlayableDirector _playableDirector;
    [Header("����������")]
    [SerializeField] private MeshRenderer _meshRenderer;
    private Texture2D _meshTexture;

    [Inject]
    public void Construct(SaveController controller)
    {
        Debug.Log("��� ���");
        _controller = controller;
        _meshTexture = new Texture2D(100, 100);
        _meshRenderer.material.mainTexture = _meshTexture;
    }

    public void Interact()
    {
        _controller.StartSaves(_playableDirector);
    }
}
