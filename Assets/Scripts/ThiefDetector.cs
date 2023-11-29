using System;
using UnityEngine;

public class ThiefDetector : MonoBehaviour
{
    public event Action Detected;
    public event Action ThiefGone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
            Detected?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
            ThiefGone?.Invoke();
    }
}
