using System;
using UnityEngine;

public class AlarmTrigger : MonoBehaviour
{
    public event Action onEnter;
    public event Action onExit;

    [SerializeField] private string _targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetTag))
            onEnter?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_targetTag))
            onExit?.Invoke();
    }
}
