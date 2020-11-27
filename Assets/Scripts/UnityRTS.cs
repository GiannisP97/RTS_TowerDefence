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

    private void Awake(){
        selectedGameobject = transform.Find("Selected").gameObject;
        selectedGameobject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        attackTimer = UnitStat.attackSpeed;
    }

    void Update(){
        attackTimer += Time.deltaTime;
        if(currentTarget!=null){
            agent.destination = currentTarget.position;

            float distance = (transform.position - currentTarget.position).magnitude;

            if(distance<= UnitStat.attackRange){
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
        if(attackTimer>=UnitStat.attackSpeed){
            r.AutoAttack(this,currentTarget.GetComponent<UnityRTS>());
            attackTimer = 0;
        }
    }

    public void TakeDamage(float dmg){
        UnitStat.HP -=dmg;
    }
}