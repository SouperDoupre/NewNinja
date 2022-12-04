using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float dmg;
    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    private Animator anim;
    private SpriteRenderer sRend;
    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(dmg);
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
                collision.GetComponent<Health>().TakeDamage(dmg);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        //turns the sprite red to notify the player the trap is triggered
        triggered = true;
        sRend.color = Color.red;
        //waits for the delay, activates trap, turns on animatio, return color to normal
        yield return new WaitForSeconds(activationDelay);
        sRend.color = Color.white;
        active = true;
        anim.SetBool("activated", true);
        //waits for delay, turns off the trap
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}
