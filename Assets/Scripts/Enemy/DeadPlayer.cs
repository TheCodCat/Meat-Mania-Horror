using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class DeadPlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;
    private SaveController _saveController;

    [Inject]
    public void Construct(SaveController saveController)
    {
        _saveController = saveController;
    }

    public void PlayDead()
    {
        _playableDirector.Play();
    }
    public void RestartGame()
    {
        _saveController.ClearData();
        LoadSceneManager.Instance.LoadSceneName("StartScene");
    }
}
