using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Controller
{
    public void AutoAttack(UnityRTS attacker,UnityRTS target){
        target.TakeDamage(attacker.UnitStat.attackDamage - target.UnitStat.def);
    }
}
