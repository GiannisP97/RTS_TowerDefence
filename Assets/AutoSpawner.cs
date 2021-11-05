using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AutoSpawner : MonoBehaviour
{
    public GameObject unit;
    public float SpawnRate;
    private float SpawnTimer;
    private player ply;
    public Image bar;
    public float fill;

    public GameObject canvas;

    public GameObject flag;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = SpawnRate;
        ply = GameObject.Find("GameController").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer += Time.deltaTime;
        if(SpawnTimer>=SpawnRate){
            GameObject o = Instantiate(unit,transform.position,Quaternion.identity);
            o.GetComponent<UnityRTS>().moveToLocation(flag.transform.position);
            ply.playersUnities.Add(o.GetComponent<UnityRTS>());
            SpawnTimer = 0;
        }

        fill = (float)SpawnTimer/(float)SpawnRate;
        bar.fillAmount = fill;
    }

    void LateUpdate(){
        canvas.transform.LookAt(canvas.transform.position + (Camera.main.transform.rotation * Vector3.back),
            Camera.main.transform.rotation * Vector3.up);
    }
}
