using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float hValue;//creates a designer chosen variable for the amount the player will heal

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().UpdateHealth(hValue);//if the healthpack touches something assigned the tag "Player" then call the UpdateHeath function in the players health component
            gameObject.SetActive(false);//this will make the healthpack disappear so it isnt able to heal again
        }
    }
}
