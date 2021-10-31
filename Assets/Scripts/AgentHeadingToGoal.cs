using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentHeadingToGoal : MonoBehaviour
{
   // public Transform goal;
    public GameObject[] paths = new GameObject[4] ;
    public NavMeshAgent agent;

    private int startingPath = 0;
    private int pathLength = 0;

    public bool canAttack = true;

    public float aggroDistance = 10;

    private bool attacking_player = false;


    void Start()
    {
        startingPath = 0;
        pathLength = paths.Length;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startingPath < pathLength)
        {
            if (Vector3.Distance(paths[startingPath].transform.position, gameObject.transform.position) < 1)
            {
                if( startingPath == pathLength - 1)
                {
                    agent.isStopped=true;
                } else
                {   //Debug.Log("checkpoint");
                    startingPath++;
                }
            }
        }

        if(canAttack){
            float min=9999999;
            UnityRTS temp_unit = null;
            foreach (UnityRTS t in GameObject.Find("GameController").GetComponent<player>().playersUnities){
                if(Vector3.Distance(t.gameObject.transform.position,transform.position)<min){
                    min = Vector3.Distance(t.gameObject.transform.position,transform.position);
                    temp_unit = t;
                }
            }

            
            if(min<aggroDistance && temp_unit!=null){
                this.GetComponent<UnityRTS>().setCurrentTarget(temp_unit.gameObject.transform);
                attacking_player = true;
            }
            else{
                attacking_player = false;
            }
        }

        if(!attacking_player){
            agent.SetDestination(paths[startingPath].transform.position);
        }
        

        if (agent.isStopped){
            //Debug.Log("I stopped");
        }

    }
}
