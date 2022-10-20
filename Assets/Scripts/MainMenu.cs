using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject levelsPanel;
    // Start is called before the first frame update
    void Start()
    {
        levelsPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableLevelsPanel(){
        levelsPanel.gameObject.SetActive(true);
    }

        public void disenableLevelsPanel(){
        levelsPanel.gameObject.SetActive(false);
    }

    public void loadlevel1(){
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }


}
