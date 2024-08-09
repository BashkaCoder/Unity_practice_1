using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private WebLoader _webLoader;

    public async void Start()
    {
        await Boot();
    }

    private async Task Boot()
    {
        var tasks = new List<Task>(3);

        await Task.WhenAll(tasks);
    }
}
