using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitRTSMenu : MonoBehaviour
{
    public UnitStats UnitStat;
    private GameObject selectedGameobject;
    private NavMeshAgent agent;
    public Transform currentTarget;
    private float attackTimer;
    private RTS_Controller r = new RTS_Controller();
    private IEnumerator coroutine;
    public Animator animator;
    private SelectUnities selectUnities;
    private Vector3 order_destination;
    private bool movingToPosition = false;
    public Owner owner;
    public string Unit_name;
    public float HP;
    public float MaxHP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public  float attackRange;
    public float healthRegen;
    public int goldCost;
    private float aggroDistance = 10;
    public bool isTower;
    private bool lockedOn = false;
    private float attack_anim_time = 0;
    private AnimatorClipInfo[] m_CurrentClipInfo;

    private float unit_radius;
    public bool commanded_to_move = false;
    private float regenTimer;

    private void Awake(){
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
        movementSpeed = UnitStat.movementSpeed;
        isTower = UnitStat.isTower;
        unit_radius = UnitStat.unit_radius;

        //Debug.Log(animator.runtimeAnimatorController.animationClips[2].name);
        //Debug.Log(attack_anim_time);

        attack_anim_time = animator.runtimeAnimatorController.animationClips[2].length;
        attack_anim_time = attack_anim_time/2; //na pernei to reference apo ton animator

        //Initiate agent
        agent.speed = movementSpeed;
        agent.radius = unit_radius;
        this.gameObject.GetComponent<CapsuleCollider>().radius = unit_radius;

        agent.stoppingDistance = 0;
        Destroy(this.gameObject,70f);
    }

    void Update(){
        attackTimer += Time.deltaTime;
        regenTimer+=Time.deltaTime;
        
        animator.SetFloat("Velocity", movementSpeed);
        //animator.SetFloat("AttackSpeed",attack_anim_time/attackSpeed);
        


        if(agent.velocity.magnitude>0 && animator.GetInteger("State")!=2){
            animator.SetInteger("State",1); // AGENT MOVING
        }

        if(agent.velocity.magnitude==0 && animator.GetInteger("State")!=2){
           animator.SetInteger("State",0); // AGENT IDLE
        }

        if(HP<=0){ // IF UNIT DIED
            animator.SetInteger("State",4);
            animator.enabled = false;
            Destroy(this.gameObject);
        }
        if(HP<MaxHP && regenTimer>=0.25){ //UNIT REGENERETION EVERY 0.25 SEC
            HP+=(float)(healthRegen/4);
            regenTimer=0;
        }

        
        


        
        if(agent.isActiveAndEnabled){
            if(agent.remainingDistance==0){
                commanded_to_move = false;
            }
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
        movingToPosition = true;
    }

    public void moveToLocation(Vector3 location){
        agent.destination = location;
        movingToPosition = true;
    }

    public void moveToPositionWithAttack(Vector3 path){
        agent.destination = transform.position + path;
    }

    public void moveToLocationWithAttack(Vector3 location){
        agent.destination = location;
    }


    public void setCurrentTarget(Transform t){
        if(t!=this.transform)
            currentTarget = t;
        else
            currentTarget=null;
    }



    public Transform getCurrentTarget(){
        return currentTarget;
    }



    public void TakeDamage(float dmg){
        HP -=dmg;
    }
}