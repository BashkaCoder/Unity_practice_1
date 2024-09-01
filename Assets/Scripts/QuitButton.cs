using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Debug.Log("Exit requested"));
        GetComponent<Button>().onClick.AddListener(() => Application.Quit());
    }
}
