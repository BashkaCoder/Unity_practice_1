using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NextSceneLoader
{
    public event Action<float> OnProgressChanged;
    public event Action OnLoadingCompleted;
    
    private bool _shouldLoad;
    private AsyncOperation _loadSceneOperation;
    
    public async UniTask LoadNextScene(CancellationToken ct)
    {
        await LoadNextSceneAsync(ct);
    }
    
    public void AllowSceneActivation() => _loadSceneOperation.allowSceneActivation = true;
    
    private async UniTask LoadNextSceneAsync(CancellationToken ct)
    {
        try
        {
            _loadSceneOperation = SceneManager.LoadSceneAsync(1);
            _loadSceneOperation.allowSceneActivation = false;
            
            while (_loadSceneOperation.progress < 0.9f)
            {
                OnProgressChanged?.Invoke(_loadSceneOperation.progress / 0.9f);
                await UniTask.Yield(ct);
            }
            OnLoadingCompleted?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            throw;
        }
    }
}