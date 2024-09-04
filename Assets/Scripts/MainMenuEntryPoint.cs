using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private LoadingOperationView _operationView;
    
    private void Start()
    {
        _operationView.StartLoading();
    }
}