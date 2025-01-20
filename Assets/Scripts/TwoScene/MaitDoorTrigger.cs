using UnityEngine;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class MaitDoorTrigger : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private PlayableDirector PlayableDirector;
    [SerializeField] private PlayerController PlayerController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerController))
        {
            if(PlayerController.IsMainKey)
                PlayableDirector.Play();
        }
    }

    public async void FinalWin()
    {
        await SceneManager.LoadSceneAsync(_sceneName).ToUniTask();
    }
}
