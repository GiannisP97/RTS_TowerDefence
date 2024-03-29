﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "AI/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Stats")]
    public float HP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public  float attackRange;
    public float healthRegenaration;

    [Header("Other")]

    public int goldCost;

    public string unit_name;

    public Sprite icon;
    public bool isTower;
    public float aggroDistance = 10;

    public float unit_radius;
}
