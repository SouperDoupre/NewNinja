using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    private Animator anim;
    private bool hit;
    private BoxCollider2D bC;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bC = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        bC.enabled = true;
         
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        if (hit) return;

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
    private void OnTriggerEnter2D(Collider2D collision)
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
    {
        hit = true;
        base.OnTriggerEnter2D(collision);//Executes logic from the parent script first
        bC.enabled = false;

        if (anim != null)
            anim.SetTrigger("explode");//When the object is a fireball it needs to explode
        else
            gameObject.SetActive(false);//then its an arrow and it doesnt explode just deactivates
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
