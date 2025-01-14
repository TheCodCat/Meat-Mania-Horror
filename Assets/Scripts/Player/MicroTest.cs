using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicroTest : MonoBehaviour
{
    [SerializeField] private Vector3 _min;
    [SerializeField] private Vector3 _max;
    private void OnEnable()
    {
        LoudnessToMicrophone.OnMicrophoneVolume += SetScale;
    }
    private void OnDisable()
    {
        LoudnessToMicrophone.OnMicrophoneVolume -= SetScale;
    }
    private void SetScale(float volume)
    {
        Vector3 scale = Vector3.Lerp(_min, _max, volume);
        transform.localScale = scale;
    }
}
