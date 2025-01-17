using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(BoxCollider))]
public class GameCutSceneTrigger : MonoBehaviour
{
    public bool IsPlayed;
    [SerializeField] private PlayableDirector _playableDirector;
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
}
