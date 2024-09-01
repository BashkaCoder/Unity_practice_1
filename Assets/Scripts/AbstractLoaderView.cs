using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractLoaderView : MonoBehaviour
{
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Slider _progressbar;
    
    protected virtual void Initialize()
    {
        _title.text = "Загрузка(Web): 0/100%";
        _progressbar.value = 0;
    }
    
    protected virtual void UpdateProgress(float progressBarValue)
    {
        _progressbar.value = progressBarValue;
        _title.text = $"Загрузка(Web): {progressBarValue * 100f:f2}/100%";
    }
}