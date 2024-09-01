using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class ResourceLoader
{
    public event Action<float> OnProgressChanged;
    public event Action<Sprite> OnLoadingCompleted;
    public async UniTask LoadImage(CancellationToken ct)
    {
        var sprite = await LoadImageAsync("8k_moon", ct);
        OnLoadingCompleted?.Invoke(sprite);
    }

    private async UniTask<Sprite> LoadImageAsync(string url, CancellationToken ct)
    {
        var request = Resources.LoadAsync<Sprite>(url);
        while (!request.isDone)
        {
            OnProgressChanged?.Invoke(request.progress);
            await UniTask.Yield(ct);
        }
        return (Sprite) request.asset;
    }
}