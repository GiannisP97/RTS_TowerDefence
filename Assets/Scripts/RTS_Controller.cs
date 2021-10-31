using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Controller
{
    public void AutoAttack(UnityRTS attacker,UnityRTS target){
        target.TakeDamage(attacker.attackDamage - target.def);
    }

    public void AttackBack(UnityRTS attacker,UnityRTS target){
        if(target.getCurrentTarget()==null)
            target.setCurrentTarget(attacker.gameObject.transform);
    }


    
}
