using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class SaveTest : MonoBehaviour, IInteractable
{
    private SaveController _controller;
    [SerializeField] private PlayableDirector _playableDirector;

    [Inject]
    public void Construct(SaveController controller)
    {
        Debug.Log("Все окк");
        _controller = controller;
    }

    public void Interact()
    {
        _controller.StartSaves(_playableDirector);
    }
}
