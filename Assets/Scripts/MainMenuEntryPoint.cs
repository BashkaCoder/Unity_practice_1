using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private TMP_Text _text;
    
    [SerializeField] private Slider _slider; 
    
    [SerializeField] private int _loadingDuration; // seconds
    [SerializeField] private int _loadingPeriod; // milliseconds

    private readonly int _clicksThreshold = 3;
    private int _clicksDone = 0;
    
    private CancellationTokenSource _ctr;
    
    private async void Start()
    {
        _ctr = new CancellationTokenSource();
        _loadingPanel.SetActive(true);
        await Boot();
    }

    public void HandleButtonClick()
    {
        if (++_clicksDone == _clicksThreshold)
        {
            _ctr.Cancel();
        }
    }
    
    private async Task Boot()
    {
        var ct = _ctr.Token;
        await LoadTextAsync(ct);
        if (ct.IsCancellationRequested)
        {
            Debug.Log("Загрузка прервана!");
            _text.text = _text.text.Insert(4, "НЕ ");
        }
        else
        {
            Debug.Log("Загрузка завершена!");
        }
        _loadingPanel.SetActive(false);
    }

    private async Task LoadTextAsync(CancellationToken ct)
    {
        await ImmitateTextLoading(_loadingDuration, _loadingPeriod, ct);
    }

    /// <param name="duration"> Длительность загружки, секунды. </param>
    /// <param name="delay"> Длительность периода ожидания в загрузке, миллисекунды. </param>
    private async Task ImmitateTextLoading(int durationS, int periodMS, CancellationToken ct)
    {
        float progress = 0;
        float delta = _slider.maxValue / (durationS * 1000f / periodMS); 

        while (progress < _slider.maxValue)
        {
            if (ct.IsCancellationRequested)
            {
                _slider.value = _slider.maxValue; 
                return;
            }
            
            progress += delta;
            _slider.value = progress;

            await Task.Delay(periodMS);
        }
        
        _slider.value = _slider.maxValue; 
    }
}