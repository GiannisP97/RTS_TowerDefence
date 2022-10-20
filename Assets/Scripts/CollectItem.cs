using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public GameObject player;

    public int Gold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.GetComponent<UnityRTS>()!=null){
            if(collision.collider.gameObject.GetComponent<UnityRTS>().owner==Owner.player){
                player.GetComponent<player>().gold+=Gold;
            }
        }

            
    }
}
