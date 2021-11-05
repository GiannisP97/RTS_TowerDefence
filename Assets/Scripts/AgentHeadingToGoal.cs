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

    public bool attacking_player = false;


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

        if(!attacking_player){
            agent.SetDestination(paths[startingPath].transform.position);
        }
        

        if (agent.isStopped){
            //Debug.Log("I stopped");
        }

        float CalculatePathLength (Vector3 targetPosition){
            // Create a path and set it based on a target position.
            NavMeshPath path = new NavMeshPath();
            if(agent.enabled)
                agent.CalculatePath(targetPosition, path);
            
            // Create an array of points which is the length of the number of corners in the path + 2.
            Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
            
            // The first point is the enemy's position.
            allWayPoints[0] = transform.position;
            
            // The last point is the target position.
            allWayPoints[allWayPoints.Length - 1] = targetPosition;
            
            // The points inbetween are the corners of the path.
            for(int i = 0; i < path.corners.Length; i++)
            {
                allWayPoints[i + 1] = path.corners[i];
            }
            
            // Create a float to store the path length that is by default 0.
            float pathLength = 0;
            
            // Increment the path length by an amount equal to the distance between each waypoint and the next.
            for(int i = 0; i < allWayPoints.Length - 1; i++)
            {
                pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
            }
            
            return pathLength;
        }

    }
}
