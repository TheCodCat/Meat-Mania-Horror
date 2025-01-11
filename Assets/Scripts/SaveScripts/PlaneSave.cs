using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using Zenject;

public class SaveTest : MonoBehaviour, IInteractable, IDrawning
{
    public bool Saves {get; private set;}
    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private FilterMode _filterMode;
    [SerializeField] private Image _proggerssBar;
    [Header("Сохранение")]
    [SerializeField] private MeshRenderer _meshRenderer;
    private SettingsDraw _settingsDraw;
    private SaveController _controller;
    private Texture2D _meshTexture;
    private int _countPixel;

    [Inject]
    public void Construct(SaveController controller, SettingsDraw settingsDraw)
    {
        Debug.Log("Все окк333");
        _settingsDraw = settingsDraw;
        _controller = controller;
        //Init();
    }
    private void Start()
    {
        Init();
    }
    public async void Draw(Vector2 uvPos)
    {
        Debug.Log("Рисую");
        int X = (int)(uvPos.x * _settingsDraw.Texture2D.width);
        int Y = (int)(uvPos.y * _settingsDraw.Texture2D.height);

        for (int y = 0; y < _settingsDraw.BrushSuze; y++)
        {
            for (int x = 0; x < _settingsDraw.BrushSuze; x++)
            {
                if(_meshTexture.GetPixel(X + x - _settingsDraw.BrushSuze / 2, Y + y - _settingsDraw.BrushSuze / 2).Equals(Color.red))
                    _meshTexture.SetPixel((X + x) - _settingsDraw.BrushSuze / 2,(Y + y) - _settingsDraw.BrushSuze / 2, Color.black);
            }
        }
        await UniTask.Yield();

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
        _meshTexture = new Texture2D(_settingsDraw.Texture2D.width,_settingsDraw.Texture2D.height);

        _meshTexture.filterMode = _filterMode;
        _meshTexture.wrapMode = TextureWrapMode.Clamp;
        _meshRenderer.material.mainTexture = _meshTexture;
        _countPixel = _meshTexture.GetPixels32().Length;
        _proggerssBar.fillAmount = 0;

        ResetTexture();
    }

    public async void ResetTexture()
    {
        for (int y = 0; y < _settingsDraw.Texture2D.width; y++)
        {
            for (int x = 0; x < _settingsDraw.Texture2D.height; x++)
            {
                _meshTexture.SetPixel(x,y, _settingsDraw.Texture2D.GetPixel(x,y));
            }
        }
        await UniTask.Yield();

        _meshTexture.Apply();
    }

    public void SetStateSaveState(bool saves)
    {
        Saves = saves;
    }

    public void Exit()
    {
        Saves = false;
        ResetTexture();
    }

}
