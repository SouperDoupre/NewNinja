using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{

    [Header("Health")]
    [SerializeField] protected float startingHealth;//Stores the health variable
    protected float currentHealth { get; private set; }//makes it so that it can be used in any script but can only be changed in this script
    protected bool dead;//checks if the player is dead

    [Header("Attack Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected int dmg;
    [SerializeField] protected float range;

    [Header("Collider Parameters")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] protected LayerMask playerLayer;
    protected float coolDownTimer = Mathf.Infinity;//gives the enemy the abilty top attack immidiately
    protected Animator anim;
    protected EnemyPatrol enemyPatrol;
    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    protected virtual void Update()
    {
        coolDownTimer += Time.deltaTime;//increments the cooldown timer by time.deltaTime on every frame
    }
    protected virtual bool PlayerinSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.y));
    }
}
