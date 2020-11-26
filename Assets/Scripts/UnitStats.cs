using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Stats")]
    public float HP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    //public float attack;
    public  float attackRange;
    [Header("Other")]

    public int goldCost;
}
