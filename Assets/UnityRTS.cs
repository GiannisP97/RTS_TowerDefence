using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnityRTS : MonoBehaviour
{
    private GameObject selectedGameobject;
    public int goldCost;
    private NavMeshAgent agent;

    private void Awake(){
        selectedGameobject = transform.Find("Selected").gameObject;
        selectedGameobject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetSelectedVisibility(bool visibility){
        selectedGameobject.SetActive(visibility);
    }

    public void moveToposition(Vector3 position){
        agent.destination = position;
    }
}
