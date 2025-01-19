using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class DeadPlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    public void PlayDead()
    {
        _playableDirector.Play();
    }
    public void RestartGame()
    {
        LoadSceneManager.Instance.LoadSceneName("StartScene");
    }
}
