using UnityEngine;
using UnityEngine.Playables;

public class WillRise : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.TryGetComponent(out CharacterController component))
        {
            _playableDirector.Play();
        }
    }
    public void LoadScene(string name)
    {
        LoadSceneManager.Instance.LoadSceneName(name);
    }
}
