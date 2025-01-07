using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown _resolution;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Toggle _toggleFullScreen;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutions.ToList().ForEach(x => _resolution.options.Add(new TMP_Dropdown.OptionData($"{x.width}x{x.height} {x.refreshRateRatio}")));

        int index = resolutions.ToList().IndexOf(Screen.currentResolution);

        _resolution.SetValueWithoutNotify(index);
        _resolution.RefreshShownValue();

        int indexQuality = QualitySettings.GetQualityLevel();
        _quality.SetValueWithoutNotify(indexQuality);
        _quality.RefreshShownValue();
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void changeFullScreen(bool full) => Screen.fullScreen = full;

    public void ChangeResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, _toggleFullScreen);
    }

    public void ChangeGlobalVolume(float value)
    {
        _audioMixer.SetFloat("AllVolume", value);
    }

}
