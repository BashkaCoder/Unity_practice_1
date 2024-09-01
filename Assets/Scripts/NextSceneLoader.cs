using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _title;
    
    private bool _shouldLoad;
    private AsyncOperation _loadSceneOperation;
    
    private void Start()
    {
        _slider.value = 0;
        _title.text = "Некст сцена загружается";
        _button.interactable = false;
        _button.onClick.AddListener(() => _loadSceneOperation.allowSceneActivation = true);
    }

    public async UniTask LoadNextScene()
    {
        await LoadNextSceneAsync(destroyCancellationToken);
    }
    
    private async UniTask LoadNextSceneAsync(CancellationToken ct)
    {
        try
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(1);
            _loadSceneOperation.allowSceneActivation = false;
            
            while (_loadSceneOperation.progress < 0.9f)
            {
                _slider.value = _loadSceneOperation.progress / 0.9f;
                await UniTask.Yield(ct);
            }
            
            _slider.value = 1f;
            _title.text = "Некст сцена загружена";
            _button.interactable = true;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            throw;
        }
    }
}