using UnityEngine;
using Cysharp.Threading.Tasks;

public class lamp : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Light _light;
    [SerializeField] private float _offset;
    private float _duraction;
    private void Start()
    {
        _offset = Random.Range(0f,5f);
    }

    private async void Update()
    {
        if (_duraction <= _curve.keys[_curve.length - 1].time)
        {
            Debug.Log(_duraction);
            _light.intensity = _curve.Evaluate(_duraction + _offset);
            _duraction += Time.deltaTime;
            await UniTask.Yield();
        }
    }
}
