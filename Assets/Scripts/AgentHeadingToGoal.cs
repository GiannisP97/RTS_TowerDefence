using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentHeadingToGoal : MonoBehaviour
{
   // public Transform goal;
    public GameObject[] paths;
    public NavMeshAgent agent;

    private int startingPath = 0;
    private int pathLength = 0;

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
                {   Debug.Log("checkpoint");
                    startingPath++;
                }
            }
        }
        
        agent.SetDestination(paths[startingPath].transform.position);

        if (agent.isStopped){
            Debug.Log("I stopped");
        }

    }
}
