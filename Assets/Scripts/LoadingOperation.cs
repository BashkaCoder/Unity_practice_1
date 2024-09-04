using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class LoadingOperation
{
    public event Action<float> OnOperationProgress;
    public event Action OnOperationCanceled;
    public event Action OnOperationFinished;
    
    private readonly CancellationTokenSource _ctr = new();

    public void StartLoading(int durationS, int periodMS)
    {
        var token = _ctr.Token;
        ImitateTextLoading(durationS, periodMS, token);
    }
    
    /// <param name="durationS"> Длительность загружки, секунды. </param>
    /// <param name="periodMS"> Длительность периода ожидания в загрузке, миллисекунды. </param>
    private async UniTask ImitateTextLoading(int durationS, int periodMS, CancellationToken ct)
    {
        int elapsedTimeMS = 0;
        while (elapsedTimeMS < durationS * 1000)
        {
            if (ct.IsCancellationRequested)
            {
                OnOperationCanceled?.Invoke();
                return;
            }
            elapsedTimeMS += periodMS;
            OnOperationProgress?.Invoke(elapsedTimeMS / (durationS * 1000f));
            await UniTask.Delay(periodMS);
        }
        OnOperationFinished?.Invoke();
    }
    
    public void Cancel()
    {
        _ctr.Cancel();
    }
}