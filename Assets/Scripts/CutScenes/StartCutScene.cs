using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    private SaveController _saveController;

    [Inject]
    public void Construct(SaveController saveController)
    {
        _saveController = saveController;
    }
    private void Start()
    {
        if (_saveController.PlayerData.CountSave.Equals(0))
        {
            _playableDirector.Play();
        }
    }
}
