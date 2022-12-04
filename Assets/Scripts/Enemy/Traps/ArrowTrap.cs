using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    private float coolDownTimer;

    private void Update()
    {
        coolDownTimer = 0;

        arrows[FindFireArrows()].transform.position = firepoint.position;
        arrows[FindFireArrows()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindFireArrows()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Attack()
    {
        coolDownTimer += Time.deltaTime;
        
        if(coolDownTimer >= attackCooldown)
            Attack();
    }
}
