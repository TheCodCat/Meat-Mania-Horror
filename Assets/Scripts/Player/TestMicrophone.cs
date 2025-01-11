using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestMicrophone : MonoBehaviour
{
    private int _samplesWindow = 32;
    [SerializeField] private AudioSource _audioClip;
    [SerializeField] private Image _image;
    [SerializeField] private float _maxLoudness;
    [SerializeField] float _loudness;
    void Start()
    {
        var name = Microphone.devices[0];
        Debug.Log(name);
        _audioClip.clip = Microphone.Start(name, true, 10, 44100);
        _audioClip.loop = true;
        _audioClip.Play();
    }
    private void Update()
    {
        _loudness = GetLoudnessFromAudioClip( _audioClip.timeSamples, _audioClip.clip);
        float field = Mathf.Clamp(-80, _maxLoudness, _loudness);
        _image.fillAmount = field;
    }
    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip audioClip)
    {
        int startPosition = clipPosition - _samplesWindow;

        if(startPosition < 0) return 0;

        float[] waveData = new float[_samplesWindow];

        audioClip.GetData(waveData, startPosition);


        float loudness = 0;
        waveData.ToList().ForEach(x => loudness += Mathf.Abs(x));

        return loudness / _samplesWindow;
    }
}
