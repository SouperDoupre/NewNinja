using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCool;//a varibale that will control how often this object can attack
    [SerializeField] private Transform firePoint;//a variable to hold the transform of this object
    [SerializeField] private GameObject[] fireballs;//a array of gameObjects that are arrows
    private Animator anim;//a variable that wil hold this object's animations
    private PlayerMove playerMove;//a variable that will hold the Playeramove component
    private float coolDownTimer = Mathf.Infinity;//a variable that tracks how long it has been for the cooldown, set at infinity

    private void Awake()
    {
        anim = GetComponent<Animator>();//assigns this objects's Animator to the variable anim
        playerMove = GetComponent<PlayerMove>();//assigns the PlayerMove component to the variable playerMove
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCool && playerMove.CanAttack())//when the player presses the assigned key
                                                                                            //and the cooldownTimer is greater that the attack cooldown and the player can attack then attack
            Attack();//calls the attack function when the conditions are met

        coolDownTimer += Time.deltaTime;//increases the coolDownTimer frame independantly
    }

    private void Attack()
    {
        anim.SetTrigger("attack");//starts the attack animation
        coolDownTimer = 0;//sets the cooldownTimer to 0
        //Object pooling the arrows
        fireballs[FindFireball()].transform.position = firePoint.position;//creates the first fireball in the array at the firepoint transform position
        fireballs[FindFireball()].GetComponent<Projectiles>().SetDirection(Mathf.Sign(transform.localScale.x));//makes sure that the fireball shoots in the direction that the player is facing
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)//this will go through the array of arrows and activate them in the hierarchy as they are called
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
