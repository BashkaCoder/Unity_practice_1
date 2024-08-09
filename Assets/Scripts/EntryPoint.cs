using System.Threading.Tasks;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private async void Start()
    {
        var task =  Task.Run(() => GetFibonacci(45));
        var result = await task;
        print(result);
    }

    private int GetFibonacci(int n)
    {
        if (n == 0)
            return 0;
        if (n == 1)
            return 1;

        return GetFibonacci(n - 1) + GetFibonacci(n - 2);
    }
}