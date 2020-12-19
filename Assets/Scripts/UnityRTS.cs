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

    public string Unit_name;
    public float MaxHP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    //public float attack;
    public  float attackRange;

    public float healthRegen;
    public int goldCost;

    private void Awake(){
        selectUnities = GameObject.Find("GameController").GetComponent<SelectUnities>();
        selectedGameobject = transform.Find("Selected").gameObject;
        selectedGameobject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        HP = UnitStat.HP;
        MaxHP = HP;
        def = UnitStat.def;
        attackDamage = UnitStat.attackDamage;
        attackSpeed = UnitStat.attackSpeed;
        attackRange = UnitStat.attackRange;
        goldCost = UnitStat.goldCost;
        attackTimer =attackSpeed;
        healthRegen = UnitStat.healthRegenaration;
        Unit_name = UnitStat.name;

    }

    void Update(){
        attackTimer += Time.deltaTime;
        if(agent.velocity.magnitude>0.1 && !animator.GetBool("attacking")){
            animator.SetInteger("State",1);
            animator.SetBool("Running",true);
        }
        else
            animator.SetBool("Running",false);

        if(!animator.GetBool("attacking") && !animator.GetBool("Running")){
            animator.SetInteger("State",0);
            animator.SetBool("Running",false);
        }

        if(HP<MaxHP){
            HP+=healthRegen*Time.deltaTime;
        }
        else
        HP = MaxHP;

        animator.SetFloat("Velocity",agent.velocity.magnitude);
        animator.SetFloat("AttackSpeed",2/attackSpeed);
        if(currentTarget!=null){
            agent.destination = currentTarget.position;
            float distance = (transform.position - currentTarget.position).magnitude;
            agent.stoppingDistance = attackRange;
            if(distance<= attackRange){
                if(attackTimer>=attackSpeed){
                    coroutine = Attack(attackSpeed*0.4f);
                    StartCoroutine(coroutine);
                    AnimatorAttack();
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

    public Vector3 calculatePath(Vector3 position){

        Vector3 path =  position - transform.position;
        return path;
    }

    public void moveToPosiotion(Vector3 path){
        agent.destination = transform.position + path;
    }



    public void setCurrentTarget(Transform t){
        if(t!=this.transform)
            currentTarget = t;
        else
            currentTarget=null;
    }

     private IEnumerator Attack(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        if(currentTarget!=null){
            r.AutoAttack(this,currentTarget.GetComponent<UnityRTS>());
        }
        
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("attacking",false);
        animator.SetInteger("State",0);
        agent.isStopped = false;
    }

    private void AnimatorAttack(){
        attackTimer = 0;
        animator.SetBool("Running",false);
        agent.isStopped = true;
        animator.SetBool("attacking",true);
        animator.SetInteger("State",2);
        

    }



    public void TakeDamage(float dmg){
        HP -=dmg;
    }
}