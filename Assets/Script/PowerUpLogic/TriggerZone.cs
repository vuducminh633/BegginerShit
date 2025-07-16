using UnityEngine;
using System;
public class TriggerZone : MonoBehaviour
{
    public Action onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPlayerEnter?.Invoke();
            Destroy(gameObject);
        }

       
    }
}
