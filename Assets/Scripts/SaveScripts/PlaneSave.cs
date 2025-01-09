using Assets.Scripts.Models;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Zenject;

public class SaveTest : MonoBehaviour, IInteractable, IDrawning
{
    private SettingsDraw _settingsDraw;
    private SaveController _controller;
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Image _proggerssBar;
    [Header("Сохранение")]
    [SerializeField] private MeshRenderer _meshRenderer;
    private Texture2D _meshTexture;
    private int _countPixel;

    [Inject]
    public void Construct(SaveController controller, SettingsDraw settingsDraw)
    {
        Debug.Log("Все окк");
        _settingsDraw = settingsDraw;
        _controller = controller;
        Init();
    }

    public void Draw(Vector2 uvPos)
    {
        Debug.Log("Рисую");
        int X = (int)(uvPos.x * _settingsDraw.SizeTexture.x);
        int Y = (int)(uvPos.y * _settingsDraw.SizeTexture.y);

        for (int y = 0; y < _settingsDraw.BrushSuze; y++)
        {
            for (int x = 0; x < _settingsDraw.BrushSuze; x++)
            {
                _meshTexture.SetPixel(X + x,Y + y, Color.black);
            }
        }

        _meshTexture.Apply();

        int count = _meshTexture.GetPixels().ToList().Where(x => x.Equals(Color.black)).Count();

        float amound = (float)count / _countPixel;
        _proggerssBar.fillAmount = amound / _settingsDraw.FillAmound;

        Debug.Log(amound);

        if (amound >= _settingsDraw.FillAmound)
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
        _meshTexture = new Texture2D(_settingsDraw.SizeTexture.x, _settingsDraw.SizeTexture.y);
        _meshTexture.filterMode = _filterMode;
        _meshTexture.wrapMode = TextureWrapMode.Clamp;

        _meshRenderer.material.mainTexture = _meshTexture;
        _countPixel = _meshTexture.GetPixels32().Length;
        _proggerssBar.fillAmount = 0;
    }
}
