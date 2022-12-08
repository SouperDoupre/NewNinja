using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TrapDoor : MonoBehaviour
{
    [SerializeField] private UnityEvent _lock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            _lock?.Invoke();
    }
}
