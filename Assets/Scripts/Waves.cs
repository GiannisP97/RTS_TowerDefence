﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Waves : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy_spawn;
    public int number_of_waves;
    public float beginning_time;
    public float resting_time;
    public float enemie_spawn_interval;
    public GameObject[] checkpoints = new GameObject[4];
    public List<GameObject>  enemies = new List<GameObject>();

    public Text wave_number;

    public Text next_wave_time;
    //private class members
    private float starting_time;
    private float wavetime;
    public bool spawning_finished = false;
    private IEnumerator coroutine;
    private int current_wave;
    private float time_left ;
    // Start is called before the first frame update
    void Start()
    {
        starting_time = Time.time;
        current_wave = number_of_waves;
        wave_number.text = current_wave.ToString();
        time_left = beginning_time;
        coroutine = Spawn_wave(beginning_time,10);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawning_finished && current_wave>1){
            Debug.Log("New wave started");
            spawning_finished = false;
            current_wave--;
            wave_number.text = current_wave.ToString();
            time_left = resting_time;
            coroutine = Spawn_wave(resting_time,10);
            StartCoroutine(coroutine);
        }
        if(time_left>0){
            time_left = time_left-Time.deltaTime;
            next_wave_time.text = time_left.ToString();
        }
        else{
            time_left = 0;
            next_wave_time.text = time_left.ToString();
        }

        /*
        foreach(GameObject e in enemies){
            if(e==null){
                //enemies.Remove(e);
            }
        }
        */

    }


     private IEnumerator Spawn_wave(float waitTime,int number_of_enemies)
    {
        yield return new WaitForSeconds(waitTime);
        if(number_of_enemies >0){
            GameObject o = Instantiate(enemy1,enemy_spawn.transform.position + new Vector3(Random.Range(0,5),0,Random.Range(0,5)),enemy_spawn.transform.rotation);
            o.GetComponent<AgentHeadingToGoal>().paths = checkpoints;
            //enemies.Add(o);
            coroutine = Spawn_wave(enemie_spawn_interval,number_of_enemies-1);
            StartCoroutine(coroutine);
        }
        else{
            spawning_finished = true;
        }



    }

}
