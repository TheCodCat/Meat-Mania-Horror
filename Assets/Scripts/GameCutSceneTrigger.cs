using UnityEngine;
using UnityEngine.Playables;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class GameCutSceneTrigger : MonoBehaviour
{
    public bool IsPlayed;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private GameObject _enemy;
    private SaveController _saveController;

    [Inject]
    public void Construct(SaveController saveController)
    {
        _saveController = saveController;
    }
    private void Start()
    {
        if(IsPlayed)
            _enemy.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayed) return;

        if(other.transform.TryGetComponent(out PlayerMover component))
        {
            IsPlayed = true;
            _playableDirector.Play();
        }
    }

    public void Init(bool Played)
    {
        IsPlayed = Played;
    }

    public void ExitCutScene()
    {
        _enemy.SetActive(false);
        _saveController.PlayerData.Triggers.Add(true);

    }
}
