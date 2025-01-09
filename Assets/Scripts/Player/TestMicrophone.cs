using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMicrophone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioClip;
    void Start()
    {
        var name = Microphone.devices[0];
        Debug.Log(name);
        _audioClip.clip = Microphone.Start(name, true,10, 44100);
        _audioClip.loop = true;
        _audioClip.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
