using UnityEngine;
using UnityEngine.Events;

public class Breakable : MonoBehaviour
{
    public UnityEvent OnBreak;

    public void Hit()
    {
        OnBreak.Invoke();
        Destroy(gameObject);
    }
}
