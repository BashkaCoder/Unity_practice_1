using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextSceneLoaderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Slider _progressbar;
    [SerializeField] private Button _button;

    private readonly NextSceneLoader _nextSceneLoader = new();
    
    private void OnEnable()
    {
        _nextSceneLoader.OnProgressChanged += UpdateProgress;
        _nextSceneLoader.OnLoadingCompleted += FinishSceneLoading;

    }
    
    private void OnDisable()
    {
        _nextSceneLoader.OnProgressChanged -= UpdateProgress;
        _nextSceneLoader.OnLoadingCompleted -= FinishSceneLoading;
    }
    
    public void Initialize()
    {
        _progressbar.value = 0;
        _title.text = "Некст сцена загружается";
        _button.interactable = false;
        _button.onClick.AddListener(_nextSceneLoader.AllowSceneActivation);
    }

    public void LoadNextScene()
    {
        _nextSceneLoader.LoadNextScene(this.GetCancellationTokenOnDestroy());
    }
    
    private void UpdateProgress(float progressBarValue)
    {
        _progressbar.value = progressBarValue;
        _title.text = $"Загрузка(Web): {progressBarValue * 100f:f2}/100%";
    }

    private void FinishSceneLoading()
    {
        _progressbar.value = 1f;
        _title.text = "Некст сцена загружена";
        _button.interactable = true;
    }
}