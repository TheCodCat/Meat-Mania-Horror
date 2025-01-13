using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class LoadSceneManager: MonoBehaviour
{

    public static LoadSceneManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public async void LoadSceneName(string name)
    {
        await SceneManager.LoadSceneAsync(name).ToUniTask();
    }
}
