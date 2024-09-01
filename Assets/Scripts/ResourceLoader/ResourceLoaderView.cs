using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceLoaderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Slider _progressbar;
    [SerializeField] private Image _image;

    private readonly ResourceLoader _resourceLoader = new();
    
    private void OnEnable()
    {
        _resourceLoader.OnProgressChanged += UpdateProgress;
        _resourceLoader.OnLoadingCompleted += SetSprite;
    }
    
    private void OnDisable()
    {
        _resourceLoader.OnProgressChanged -= UpdateProgress;
        _resourceLoader.OnLoadingCompleted -= SetSprite;
    }
    
    public void Initialize()
    {
        _title.text = "Загрузка(Web): 0/100%";
        _progressbar.value = 0;
    }

    public void LoadImage()
    {
        _resourceLoader.LoadImage(this.GetCancellationTokenOnDestroy());
    }
    
    private void UpdateProgress(float progressBarValue)
    {
        _progressbar.value = progressBarValue;
        _title.text = $"Загрузка(Web): {progressBarValue * 100f:f2}/100%";
    }

    private void SetSprite(Sprite sprite)
    {
        _title.text = "Загрузка(Web): 100/100%";
        _progressbar.value = 1f;
        _image.sprite = sprite;
    }
}