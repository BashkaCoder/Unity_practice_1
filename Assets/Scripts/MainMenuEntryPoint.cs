using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private WebLoaderView _webLoaderView;
    [SerializeField] private ResourceLoaderView _resourceLoaderView;
    [SerializeField] private NextSceneLoaderView _sceneLoaderView;

    private void Start()
    {
        Boot();
    }

    private void Boot()
    {
        _webLoaderView.Initialize();
        _webLoaderView.LoadImage();
        _resourceLoaderView.Initialize();
        _resourceLoaderView.LoadImage();
        _sceneLoaderView.Initialize();
        _sceneLoaderView.LoadNextScene();
    }
}