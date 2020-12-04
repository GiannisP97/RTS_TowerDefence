using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityRTS : MonoBehaviour
{
    public UnitStats UnitStat;
    private GameObject selectedGameobject;
    private NavMeshAgent agent;
    private Transform currentTarget;
    private float attackTimer;
    private RTS_Controller r = new RTS_Controller();

    public float HP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    //public float attack;
    public  float attackRange;
    public int goldCost;

    private void Awake(){
        selectedGameobject = transform.Find("Selected").gameObject;
        selectedGameobject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        HP = UnitStat.HP;
        def = UnitStat.def;
        attackDamage = UnitStat.attackDamage;
        attackSpeed = UnitStat.attackSpeed;
        attackRange = UnitStat.attackRange;
        goldCost = UnitStat.goldCost;
        attackTimer =attackSpeed;
    }

    void Update(){
        attackTimer += Time.deltaTime;
        if(currentTarget!=null){
            agent.destination = currentTarget.position;

            float distance = (transform.position - currentTarget.position).magnitude;

            if(distance<= attackRange){
                Attack();
            }


        }
    }

    public void SetSelectedVisibility(bool visibility){
        selectedGameobject.SetActive(visibility);
    }

    public void moveToposition(Vector3 position){
        agent.destination = position;
    }

    public void setCurrentTarget(Transform t){
        currentTarget = t;
    }

    private void Attack(){
        if(attackTimer>=attackSpeed){
            r.AutoAttack(this,currentTarget.GetComponent<UnityRTS>());
            attackTimer = 0;
        }
    }

    public void TakeDamage(float dmg){
        HP -=dmg;
    }
}