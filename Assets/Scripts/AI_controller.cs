using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AI_controller : MonoBehaviour
{

    public float AvoidenceTimePredict;
    public int iterationPerFrame;
    // Start is called before the first frame update
    void Start()
    {
        NavMesh.avoidancePredictionTime = AvoidenceTimePredict;
        NavMesh.pathfindingIterationsPerFrame = iterationPerFrame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
