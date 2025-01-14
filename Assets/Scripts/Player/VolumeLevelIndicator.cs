using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class VolumeLevelIndicator : MonoBehaviour
{
    [SerializeField] private Image _indicator;
    [SerializeField] private float _minLevel;
    [SerializeField] private float _maxLevel;

    private void OnEnable()
    {
        LoudnessToMicrophone.OnMicrophoneVolume += SetLevelIndicator;
    }
    private void OnDisable()
    {
        LoudnessToMicrophone.OnMicrophoneVolume -= SetLevelIndicator;
    }
    public void SetLevelIndicator(float volume)
    {
        float fill = Mathf.Lerp(_minLevel,_maxLevel, volume) * Time.deltaTime;

        _indicator.fillAmount = fill;
        Debug.Log(fill);
    }
}
