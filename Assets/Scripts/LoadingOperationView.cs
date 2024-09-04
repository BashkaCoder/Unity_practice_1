using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingOperationView : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _cancellationButton;
    [SerializeField] private Slider _progressbar;

    [SerializeField] private int _loadingDuration; // seconds
    [SerializeField] private int _loadingPeriod; // milliseconds
    
    private readonly LoadingOperation _operation = new();
    
    private readonly int _clicksToSkip = 3;
    private int _clicksDone = 0;
    
    private void Awake()
    {
        _cancellationButton.onClick.AddListener(HandleButtonClick);
    }

    private void OnEnable()
    {
        _operation.OnOperationProgress += HandleOperationProgress;
        _operation.OnOperationFinished += HandleOperationCompletion;
        _operation.OnOperationCanceled += HandleOperationCancellation;
    }

    private void OnDisable()
    {
        _operation.OnOperationProgress -= HandleOperationProgress;
        _operation.OnOperationFinished -= HandleOperationCompletion;
        _operation.OnOperationCanceled -= HandleOperationCancellation;
    }

    public void StartLoading()
    {
        _operation.StartLoading(_loadingDuration, _loadingPeriod);
    }

    private void HandleOperationProgress(float progress)
    {
        _progressbar.value = Mathf.Clamp01(progress);
    }

    private void HandleOperationCompletion()
    {
        Debug.Log("Загрузка завершена!");
        _progressbar.value = 1.0f;
        _loadingPanel.SetActive(false);
    }
    
    private void HandleOperationCancellation()
    {
        Debug.Log("Загрузка прервана!");
        _text.text = _text.text.Insert(4, "НЕ ");
        _loadingPanel.SetActive(false);
    }
    
    private void HandleButtonClick()
    {
        if (++_clicksDone == _clicksToSkip)
        {
            _operation.Cancel();
        }
    }
}