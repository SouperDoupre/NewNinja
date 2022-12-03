using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField] private float speed;//creates a designer chosen speed the projectile will move
    private bool hit;//a bool to check if the projectile hit something
    private float direction;//creates a variable to determine which direction the projectile is moveing
    private float lifetime;//creates a variable that will determine how long the projectile will be enabled

    private BoxCollider2D boxCollider;//creates a variable that will hold the boxCollider componenet
    private Animator anim;//creates a variable that will hold the animator component

    private void Awake()
    {
        anim = GetComponent<Animator>();//assigns the Animator to the anim variable
        boxCollider = GetComponent<BoxCollider2D>();//assigns the boxCollider to the boxCollider variable
    }

    private void Update()
    {
        if (hit) return;//if the projectile hit something the return true
        float mSpeed = speed * Time.deltaTime * direction;//calculates how fast the projectile will move
        transform.Translate(mSpeed, 0, 0);//calculates how the projectile looks while moving
        lifetime += Time.deltaTime;//decreases the lifetime of the projectile 
        if (lifetime > 5) gameObject.SetActive(false);//when the projectiles lifetime is up it will deactivate
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;//returns true if the projectile collides with something
        boxCollider.enabled = false;//disables the boxcollider so that i doesnt continually deal damage
        anim.SetTrigger("explode");//starts the explode animation

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().UpdateHealth(-1);
    }
    public void SetDirection(float _direction)
    {
        lifetime = 0;//sets how long the projectile will live
        direction = _direction;//sets the direction that the fireball was fired in
        gameObject.SetActive(true);//activates the projectile so that it can interact with other objects
        hit = false;//the projectile hasnt hit anything yet
        boxCollider.enabled = true;//turns on the boxcollider so that i can detect collisions

        float localScaleX = transform.localScale.x;//cahnges how the sprite looks when the fireball is fired
        if (Mathf.Sign(localScaleX) != _direction) 
            localScaleX = -localScaleX;//if the fireball is shot to the left then if will flip it on the x axis

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);//otherwise it will look the same going right
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
