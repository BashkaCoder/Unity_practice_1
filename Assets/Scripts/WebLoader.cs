using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class WebLoader
{
    public event Action<float> OnProgressChanged;
    public event Action<Sprite> OnLoadingCompleted;
    
    public async UniTask LoadImage(CancellationToken ct)
    {
        var sprite = await LoadImageAsync("https://www.solarsystemscope.com/textures/download/8k_earth_daymap.jpg", ct);
        OnLoadingCompleted?.Invoke(sprite);
    }
    
    private async UniTask<Sprite> LoadImageAsync(string url, CancellationToken ct)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        request.SendWebRequest();
        while (!request.isDone)
        {
            OnProgressChanged?.Invoke(request.downloadProgress);
            await UniTask.Yield(ct);
        }
        
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning(request.error);
            throw new NullReferenceException("UnityWebRequestTexture failed");
        }
        else
        {
            var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        } 
    }
}
