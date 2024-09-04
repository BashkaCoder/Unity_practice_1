using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using Cysharp.Threading.Tasks;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _cancellationButton;
    [SerializeField] private Slider _progressbar; 
    
    [SerializeField] private int _loadingDuration; // seconds
    [SerializeField] private int _loadingPeriod; // milliseconds

    private readonly int _clicksToSkip = 3;
    private int _clicksDone = 0;
    
    private CancellationTokenSource _ctr;

    private void Awake()
    {
        _cancellationButton.onClick.AddListener(HandleButtonClick);
    }

    private void Start()
    {
        _ctr = new CancellationTokenSource();
        _loadingPanel.SetActive(true);
        Boot();
    }

    private void HandleButtonClick()
    {
        if (++_clicksDone == _clicksToSkip)
        {
            _ctr.Cancel();
        }
    }
    
    private async UniTaskVoid Boot()
    {
        var ct = _ctr.Token;
        await ImmitateTextLoading(_loadingDuration, _loadingPeriod, ct);
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

    /// <param name="durationS"> Длительность загружки, секунды. </param>
    /// <param name="periodMS"> Длительность периода ожидания в загрузке, миллисекунды. </param>
    private async UniTask ImmitateTextLoading(int durationS, int periodMS, CancellationToken ct)
    {
        float progress = 0;
        float delta = _progressbar.maxValue / (durationS * 1000f / periodMS); 

        while (progress < _progressbar.maxValue)
        {
            if (ct.IsCancellationRequested)
            {
                _progressbar.value = _progressbar.maxValue; 
                return;
            }
            
            progress += delta;
            _progressbar.value = progress;

            await UniTask.Delay(periodMS);
        }
        
        _progressbar.value = _progressbar.maxValue; 
    }
}