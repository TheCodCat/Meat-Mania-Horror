using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;

public class Settings : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown _resolution;
    [SerializeField] private TMP_Dropdown _quality;
    [SerializeField] private Toggle _toggleFullScreen;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _micro;
    public static Action OnActiveMicro;
    public static bool _micros;
    public static bool Micro
    {
        get
        {
            return _micros;
        }
        set
        {
            _micros = value;
            OnActiveMicro?.Invoke();
        }
    }

    private float _minV = -60;
    private float _maxV = 0;

    private void OnEnable()
    {
        if (!Microphone.devices.Length.Equals(0))
        {
            Micro = true;
            _micro.interactable = true;
            _micro.enabled = true;
        }
    }
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

        _audioMixer.GetFloat("AllVolume", out float volume);
        Debug.Log(Mathf.Lerp(_maxV,_minV,volume));

        _slider.SetValueWithoutNotify(Mathf.Lerp(_maxV,_minV, volume));
    }

    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void changeFullScreen(bool full)
    {
        Screen.fullScreen = full;
    }

    public void ChangeResolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, _toggleFullScreen.isOn);
    }

    public void ChangeGlobalVolume(float value)
    {
        _audioMixer.SetFloat("AllVolume", value);
    }

}
