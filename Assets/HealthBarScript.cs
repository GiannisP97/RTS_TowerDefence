using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBarScript : MonoBehaviour
{
    public UnityRTS unit;

    public event Action <float> OnhealthPctChange = delegate{};
    private float MaxHealth;

    [SerializeField]
    private float updateSpeedSeconds;
    public Image bar;
    public float fill;
    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = unit.HP;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        float preChangePct = bar.fillAmount;
        float elapsed = 0f;

        while(elapsed<updateSpeedSeconds){
            elapsed+=Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(preChangePct,pct,elapsed/updateSpeedSeconds);
        }
        */
    fill = (float)unit.HP/(float)MaxHealth;
    bar.fillAmount = fill;

        
    }

    void LateUpdate(){
        transform.LookAt(transform.position + (Camera.main.transform.rotation * Vector3.back),
            Camera.main.transform.rotation * Vector3.up);
    }


}
