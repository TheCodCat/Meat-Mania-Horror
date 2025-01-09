using Assets.Scripts.Models;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class SaveTest : MonoBehaviour, IInteractable, IDrawning
{
    private SaveController _controller;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private int _wight;
    [SerializeField] private int _height;
    [SerializeField] private FilterMode _filterMode;
    [Header("Сохранение")]
    [SerializeField, Range(0, 1)] private float _fillAmound;
    [SerializeField] private MeshRenderer _meshRenderer;
    private Texture2D _meshTexture;
    private int _countPixel;

    [Inject]
    public void Construct(SaveController controller)
    {
        Debug.Log("Все окк");
        _controller = controller;
        Init();
    }

    public void Draw(Vector2 uvPos)
    {
        Debug.Log("Рисую");
        int X = (int)(uvPos.x * _wight);
        int Y = (int)(uvPos.y * _height);

        _meshTexture.SetPixel(X,Y, Color.black);
        _meshTexture.Apply();

        int count = _meshTexture.GetPixels().ToList().Where(x => x.Equals(Color.black)).Count();

        float amound = (float)count / _countPixel;

        if (amound.Equals(_fillAmound))
        {
            _controller.SaveGud();
        }
    }

    public void Interact()
    {
        _controller.StartSaves(_playableDirector);
    }

    public void Init()
    { 
        _meshTexture = new Texture2D(_wight, _height);
        _meshTexture.filterMode = _filterMode;
        _meshTexture.wrapMode = TextureWrapMode.Clamp;

        _meshRenderer.material.mainTexture = _meshTexture;
        _countPixel = _meshTexture.GetPixels32().Length;
    }
}
