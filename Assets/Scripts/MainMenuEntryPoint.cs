using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private AssetLoaderView _webLoaderView;
    [SerializeField] private ResourceLoader _resourceLoader;
    [SerializeField] private NextSceneLoader _sceneLoader;

    private void Start()
    {
        Boot();
    }

    private void Boot()
    {
        _webLoaderView.Initialize();
        _webLoaderView.LoadImage();
        _resourceLoader.LoadImage();
        _sceneLoader.LoadNextScene();
    }
}