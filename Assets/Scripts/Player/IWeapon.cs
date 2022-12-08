using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    [SerializeField] float Dmg { get; set; }
    [SerializeField] float attackCool { get; set; }
}
