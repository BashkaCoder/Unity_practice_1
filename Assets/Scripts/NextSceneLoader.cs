using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Button _button;

    private bool shouldLoad;
    
    private void Start()
    {
        _slider.value = 0;
        _button.interactable = false;
        _button.onClick.AddListener(() => shouldLoad = true);
    }

    public async Task LoadNextScene()
    {
        var operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            await Task.Yield();
            _slider.value = operation.progress;

            if (operation.progress >= 0.9f)
                _button.interactable = true;
            
            if (operation.progress >= 0.9f && shouldLoad)
                operation.allowSceneActivation = true;
        }
        
    }
}
