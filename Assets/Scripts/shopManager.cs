using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class shopManager : MonoBehaviour
{
    public Transform grid;
    private player player;
    private Image[] images;
    public List<UnityRTS> unitsAvailable;
    public GameObject Spawn;
    public Text Gold;
    // Start is called before the first frame update
    void Start()
    {
        images = grid.GetComponentsInChildren<Image>();
        player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        Gold.text = player.gold.ToString();
    }

    public void BuyUnit(){
        if(player.gold >= unitsAvailable[0].goldCost){
            UnityRTS u = Instantiate(unitsAvailable[0],Spawn.transform.position,new Quaternion());
            player.gold-=unitsAvailable[0].goldCost;
            player.playersUnities.Add(u);
        }
    }

    public void BuyUnit2(){
        if(player.gold >= unitsAvailable[0].goldCost){
            UnityRTS u = Instantiate(unitsAvailable[1],Spawn.transform.position,new Quaternion());
            player.gold-=unitsAvailable[0].goldCost;
            player.playersUnities.Add(u);
        }
    }
}
