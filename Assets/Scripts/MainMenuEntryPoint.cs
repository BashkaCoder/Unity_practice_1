using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private WebLoader _webLoader;
    [SerializeField] private ResourceLoader _resourceLoader;
    [SerializeField] private NextSceneLoader _sceneLoader;

    public async void Start()
    {
        await Boot();
    }

    private async Task Boot()
    {
        var tasks = new List<Task>(3);
        tasks.Add(_webLoader.LoadImage());
        tasks.Add(_resourceLoader.LoadImage());
        tasks.Add(_sceneLoader.LoadNextScene());

        await Task.WhenAll(tasks);
    }
}
