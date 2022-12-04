using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;//Stores the health variable
    public float currentHealth { get; private set; }//makes it so that it can be used in any script but can only be changed in this script
    private Animator anim;//sets the animator variable in Anim
    private bool dead;//checks if the player is dead

    [Header("IFrames")]
    [SerializeField] private float iFrameDuration;//stores the variable of how long the palyer will be in iframes
    [SerializeField] private int numberOfFlash;//stores how many times the player will flash while in iframes
    private SpriteRenderer sRend;//stores the Sprite Renderer variable in sRend
    private bool invulnerable;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

 
    private void Awake()
    {
        currentHealth = startingHealth;//starts the game with current health at maximum
        anim = GetComponent<Animator>();//attaches the Animator to the variable anim
        sRend = GetComponent<SpriteRenderer>();//attaches the Sprite Renderer to the variable sRend
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
       currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);//applies damage or healing using _damage without putting the current health below 0

       if(currentHealth > 0)
       {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
       }
       else
       {
            if (!dead)
            {
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");//if the target is not dead and health is below 0 then start the "die" animation
                //deactivates all components
                foreach(Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;//sets the bool dead to true
            }
       }
    }
    public void AddHealth(float health)
    {
        currentHealth = Mathf.Clamp(currentHealth + health, 0, startingHealth);
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);//allows the target to ignore anything on assigned to layers 8 and 9 and sets it to true
        for (int i = 0; i < numberOfFlash; i++)
        {
            sRend.color = new Color(1, 0, 0, 0.5f);//changes the players color to red with 50% opacity
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));//returns after the duration of the iframes divided by double the numberofFlashes 
            sRend.color = Color.white;//after the time has elapsed then change the players color back to white with no opacity
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlash * 2));//returns after the duration of the iframes divided by double the numberofFlashes 
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);//at the end of the coroutine the player will no longer be able to ignore layers 8 and 9
        invulnerable = false;
    }
    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
