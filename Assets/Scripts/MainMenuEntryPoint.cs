using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private WebLoader _webLoader;
    [SerializeField] private ResourceLoader _resourceLoader;
    [SerializeField] private NextSceneLoader _sceneLoader;

    private void Start()
    {
        Boot();
    }

    private void Boot()
    {
        _webLoader.LoadImage();
        _resourceLoader.LoadImage();
        _sceneLoader.LoadNextScene();
    }
}
