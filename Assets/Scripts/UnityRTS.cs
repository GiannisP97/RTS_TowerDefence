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
    private IEnumerator coroutine;
    public Animator animator;
    private SelectUnities selectUnities;

    public float HP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    //public float attack;
    public  float attackRange;
    public int goldCost;

    private void Awake(){
        selectUnities = GameObject.Find("GameController").GetComponent<SelectUnities>();
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
        if(agent.velocity.magnitude>0.1){
            animator.SetInteger("State",1);
            animator.SetBool("Running",true);
        }
        else if(!animator.GetBool("attacking")){
            animator.SetInteger("State",0);
            animator.SetBool("Running",false);
        }

        animator.SetFloat("Velocity",agent.velocity.magnitude);
        if(currentTarget!=null){
            agent.destination = currentTarget.position;
            float distance = (transform.position - currentTarget.position).magnitude;
            agent.stoppingDistance = attackRange;
            if(distance<= attackRange){
                if(attackTimer>=attackSpeed){
                    coroutine = Attack(0.8f);
                    StartCoroutine(coroutine);
                }
            }
        }
        else
            agent.stoppingDistance = 0;

        if(HP<=0){
            selectUnities.UnitDied(this);
            Destroy(this.gameObject);
        }

    }

    public void SetSelectedVisibility(bool visibility){
        selectedGameobject.SetActive(visibility);
    }

    public void moveToposition(Vector3 position){
        agent.destination = position;
    }

    public void setCurrentTarget(Transform t){
        if(t!=this.transform)
            currentTarget = t;
        else
            currentTarget=null;
    }

     private IEnumerator Attack(float waitTime)
    {
        agent.velocity = new Vector3();
        if(animator.GetBool("Running")){
            animator.SetBool("Running",false);
        }
        agent.isStopped = true;
        animator.SetBool("attacking",true);
        animator.SetInteger("State",2);
        attackTimer = 0;

        yield return new WaitForSeconds(waitTime);

        if(currentTarget!=null){
            r.AutoAttack(this,currentTarget.GetComponent<UnityRTS>());
        }
        animator.SetBool("attacking",false);
        agent.isStopped = false;
    }



    public void TakeDamage(float dmg){
        HP -=dmg;
    }
}