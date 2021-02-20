using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleScript : MonoBehaviour
{
    public int lives_left = 20;
    public Text lives;
    // Start is called before the first frame update
    void Start()
    {
        lives.text = lives_left.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<UnityRTS>()!=null && other.gameObject.tag=="enemy"){
            Destroy(other.gameObject);
            lives_left--;
            lives.text = lives_left.ToString();
        }
    }
}
