using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableInstaller", menuName = "Installers/ScriptableInstaller")]
public class ScriptableInstaller : ScriptableObjectInstaller<ScriptableInstaller>
{
    [SerializeField] private SettingsDraw SettingsDraw;
    public override void InstallBindings()
    {
        Container.Bind<SettingsDraw>().FromInstance(SettingsDraw);
    }
}