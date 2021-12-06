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

    public void BuyUnit(int idex){
        if(player.gold >= unitsAvailable[idex].goldCost){
            UnityRTS u = Instantiate(unitsAvailable[idex],Spawn.transform.position,new Quaternion());
            player.gold-=unitsAvailable[idex].UnitStat.goldCost;
            player.playersUnities.Add(u);
        }
    }
}
