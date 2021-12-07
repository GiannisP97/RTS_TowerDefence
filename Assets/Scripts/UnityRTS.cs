using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Owner{player,enemy};

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
    private Vector3 order_destination;
    private bool movingToPosition = false;
    public Owner owner;
    public float HP;
    public string Unit_name;
    public float MaxHP;
    public float def;
    public float attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public  float attackRange;
    public float healthRegen;
    public int goldCost;
    public float aggroDistance = 10;
    public bool isTower = false;
    
    private float attack_anim_time = 0;
    private AnimatorClipInfo[] m_CurrentClipInfo;

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
        movementSpeed = UnitStat.movementSpeed;

        //Debug.Log(animator.runtimeAnimatorController.animationClips[2].name);

        attack_anim_time = animator.runtimeAnimatorController.animationClips[2].length;
        attack_anim_time = attack_anim_time/2;
        //Debug.Log(attack_anim_time);
    }

    void Update(){
        attackTimer += Time.deltaTime;
        
        animator.SetFloat("Velocity",movementSpeed/5);
        //animator.SetFloat("AttackSpeed",attack_anim_time/attackSpeed);
        agent.speed = movementSpeed;


        if(agent.velocity.magnitude>0 && animator.GetInteger("State")!=2){
            animator.SetInteger("State",1);
        }

        if(agent.velocity.magnitude==0 && animator.GetInteger("State")!=2){
           animator.SetInteger("State",0);
        }


        if(HP<MaxHP){
            HP+=healthRegen*Time.deltaTime; //thelei allagi, na ginei ana 0.25 sec
        }

        
        if(currentTarget!=null){
            agent.destination = currentTarget.position;
            float distance = Vector3.Distance(transform.position,currentTarget.position);
            agent.stoppingDistance = attackRange;
            if(distance<= attackRange){
                if(attackTimer>=attackSpeed && animator.GetInteger("State")!=2){
                    //Debug.Log(attack_anim_time);
                    StartCoroutine(Attack(attack_anim_time));
                }
            }
        }
        else{
            agent.stoppingDistance = 0;
        }
        
        float dist=agent.remainingDistance; 
        if(dist!=Mathf.Infinity && dist==0){
            movingToPosition = false;
        }
        if(HP<=0){
            selectUnities.UnitDied(this);
            animator.SetInteger("State",4);
            animator.enabled = false;
            Destroy(this.gameObject);
        }

        if(owner==Owner.enemy){
            float min=9999999;
            UnityRTS temp_unit = null;
            foreach (UnityRTS t in GameObject.Find("GameController").GetComponent<player>().playersUnities)
            {
                if(Vector3.Distance(t.gameObject.transform.position,transform.position)<min){
                    //min = CalculatePathLength(t.gameObject.transform.position);
                    min = Vector3.Distance(t.gameObject.transform.position,transform.position);
                    temp_unit = t;
                }
            }

            
            if(min<aggroDistance && temp_unit!=null){
                setCurrentTarget(temp_unit.gameObject.transform);
                this.GetComponent<AgentHeadingToGoal>().attacking_player = true;
            }
            else{
                this.GetComponent<AgentHeadingToGoal>().attacking_player = false;
            }
        }

        if(owner==Owner.player && !movingToPosition){
            GameObject temp_unit = null;
            float min = 99999999;
            foreach (GameObject t in GameObject.Find("Spawn Enemies").GetComponent<Waves>().enemies){
                if(Vector3.Distance(t.transform.position,transform.position)<min){
                    min = Vector3.Distance(t.transform.position,transform.position);
                    temp_unit = t;
                }
            }

            if(min<aggroDistance && temp_unit!=null){
                setCurrentTarget(temp_unit.gameObject.transform);
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

     private IEnumerator Attack(float waitTime)
    {
        attackTimer = 0;
        agent.isStopped = true;
        animator.SetInteger("State",2);
        yield return new WaitForSeconds(waitTime);
        
        if(currentTarget!=null){
            r.AutoAttack(this,currentTarget.GetComponent<UnityRTS>());
        }
        animator.SetInteger("State",0);
        agent.isStopped = false;
    }


    public Transform getCurrentTarget(){
        return currentTarget;
    }



    public void TakeDamage(float dmg){
        HP -=dmg;
    }
}