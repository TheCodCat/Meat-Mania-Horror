using System;
using System.Linq;
using UnityEngine;

public class LoudnessToMicrophone : MonoBehaviour
{
    public static Action<float> OnMicrophoneVolume;
    [SerializeField] private AudioClip _audioClip;
    private int _samplesWindow = 32;
    private float _loudness;
    public float Loudness
    {
        get
        {
            return _loudness;
        }
        set
        {
            _loudness = value;
            OnMicrophoneVolume?.Invoke(value);
        }
    }

    [SerializeField] private float _threslod;
    [SerializeField] private float _sensityvity;

    private void OnEnable()
    {
        Settings.OnActiveMicro += InitMicrophone;
    }
    private void OnDisable()
    {
        Settings.OnActiveMicro -= InitMicrophone;
    }

    private void Start()
    {
        if(Settings.Micro)
            InitMicrophone();
    }

    void InitMicrophone()
    {
        try
        {
            var name = Microphone.devices[0];
            _audioClip = Microphone.Start(name, true, 20, AudioSettings.outputSampleRate);
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("�������� �� ������");
        }
    }

    private void Update()
    {
        if(Settings.Micro)
            Loudness = GetLoudnessToMicrophone();
    }

    public float GetLoudnessToMicrophone()
    {
        try
        {
            float loudness = GetLoudnessFromAudioClip(Microphone.GetPosition(Microphone.devices[0]), _audioClip);
            return loudness > _threslod ? loudness * _sensityvity : 0;
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            return 0;
        }
    }

    public float GetLoudnessFromAudioClip(int clipPosition, AudioClip audioClip)
    {
        int startPosition = clipPosition - _samplesWindow;

        if(startPosition < 0) return 0;

        float[] waveData = new float[_samplesWindow];

        audioClip.GetData(waveData, startPosition);


        float loudness = 0;

        waveData.ToList().ForEach(x =>
        {
            loudness += Mathf.Abs(x);
        });

        return loudness / _samplesWindow;
    }
}
