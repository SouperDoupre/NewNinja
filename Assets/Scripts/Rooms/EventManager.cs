using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField] private GameObject doorLock;
    private void Awake()
    {
        doorLock.SetActive(false);
    }
    public void LockDoor()
    {
        doorLock.SetActive(true);
    }
}
