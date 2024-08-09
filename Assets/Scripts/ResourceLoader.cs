using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class ResourceLoader : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _title;

    public void Start()
    {
        _slider.value = 0;
    }

    public async Task LoadImage()
    {
        await LoadImage("8k_moon");
    }
    
    private async Task LoadImage(string url)
    {
        var request = Resources.LoadAsync<Sprite>(url);
        while (!request.isDone)
        {
            await Task.Yield();
            _slider.value = request.progress;
            _title.text = String.Format("Загрузка(Resources): {0:f2}/100%", request.progress * 100f);
        }
        
        _slider.value = 1f;
        
        _image.sprite = (Sprite) request.asset;
    }
}