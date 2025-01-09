using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class LoadSceneManager: MonoBehaviour
{
    public async void LoadSceneName(string name)
    {
        await SceneManager.LoadSceneAsync(name).ToUniTask();
    }
}
