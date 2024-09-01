using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class ResourceLoader : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _title;

    public void Start()
    {
        _slider.value = 0;
    }

    public async UniTask LoadImage()
    {
        await LoadImageAsync("8k_moon", this.destroyCancellationToken);
    }

    private async UniTask LoadImageAsync(string url, CancellationToken ct)
    {
        var request = Resources.LoadAsync<Sprite>(url);
        while (!request.isDone)
        {
            await UniTask.Yield(ct);
            _slider.value = request.progress;
            _title.text = String.Format("Загрузка(Resources): {0:f2}/100%", request.progress * 100f);
        }

        _slider.value = 1f;
        _image.sprite = (Sprite) request.asset;
    }
}