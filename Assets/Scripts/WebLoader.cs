using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using TMPro;

public class WebLoader : MonoBehaviour
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
        await LoadImage("https://www.solarsystemscope.com/textures/download/8k_earth_daymap.jpg");
    }
    
    private async Task LoadImage(string url)
    {
        var request = UnityWebRequestTexture.GetTexture(url);
        request.SendWebRequest();
        while (!request.isDone)
        {
            _slider.value = request.downloadProgress;
            _title.text = String.Format("Загрузка(Web): {0:f2}/100%", request.downloadProgress * 100f);
            await Task.Yield();
        }
        
        _slider.value = 1f;
        
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogWarning(request.error);
        }
        else
        {
            var texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            _image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        } 
    }
}
