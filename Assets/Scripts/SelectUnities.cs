using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUnities : MonoBehaviour
{
    public Transform stats_panel;

    public Transform name_panel;
    public GameObject arrows;
    //public Camera cam;
    private Vector3 startposition;
    private Vector3 endPosition;
    private Vector3 startMousePosition;
    private player player;
    private GameObject arrow_temp = null;
    // Start is called before the first frame update
    private List<UnityRTS> selectedUnities;

    private BuildingSpawner building;

    private Text health;
    private Text unitName;
    private Text attackDamage;
    private Text attackSpeed;
    private Text attackRange;
    private Text healthRegen;
    private Text defence;
    private Image Icon;
    private Image healthbar;
    [SerializeField] private RectTransform selectionAreaTransform;
    void Awake()
    {
        selectedUnities = new List<UnityRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
        player = GetComponent<player>();
        healthRegen = stats_panel.Find("reganaretion value").GetComponent<Text>();
        attackDamage = stats_panel.Find("attack damage value").GetComponent<Text>();
        attackSpeed = stats_panel.Find("attack speed value").GetComponent<Text>();
        attackRange = stats_panel.Find("attack range value").GetComponent<Text>();
        defence = stats_panel.Find("defence value").GetComponent<Text>();

        health = name_panel.Find("health").GetComponent<Text>();
        unitName = name_panel.Find("unitName").GetComponent<Text>();
        Icon = name_panel.Find("unit_icon").GetComponent<Image>();
        healthbar = name_panel.Find("foreground").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        update_health_UI();
        if(Input.GetMouseButtonDown(0)){
            selectionAreaTransform.gameObject.SetActive(true);
            startMousePosition  = Input.mousePosition;
            building = null;
        }
        if(Input.GetMouseButton(0)){
            Vector3 s =Input.mousePosition;
            Vector3 lowerleft = new Vector3(Mathf.Min(startMousePosition.x,s.x),Mathf.Min(startMousePosition.y,s.y));
            Vector3 upperRight = new Vector3(Mathf.Max(startMousePosition.x,s.x),Mathf.Max(startMousePosition.y,s.y));
            selectionAreaTransform.position = lowerleft;
            selectionAreaTransform.sizeDelta = new Vector2(upperRight.x - lowerleft.x,upperRight.y - lowerleft.y);
            //selectionAreaTransform.localScale = upperRight - lowerleft;
        }
        if(Input.GetMouseButtonUp(0)){
            Vector3 s = Input.mousePosition;
            selectionAreaTransform.gameObject.SetActive(false);
            Vector2 lowerleft = new Vector3(Mathf.Min(startMousePosition.x,s.x),Mathf.Min(startMousePosition.y,s.y));
            Vector2 upperRight = new Vector3(Mathf.Max(startMousePosition.x,s.x),Mathf.Max(startMousePosition.y,s.y));
            foreach(UnityRTS u in selectedUnities){
                u.SetSelectedVisibility(false);
            }
            selectedUnities.Clear();
            foreach(UnityRTS unit in player.playersUnities){

                Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

                if(screenPos.x > lowerleft.x && screenPos.x<upperRight.x && screenPos.y>lowerleft.y && screenPos.y<upperRight.y){
                    selectedUnities.Add(unit);
                    unit.SetSelectedVisibility(true);
                }

            
            }
            update_stats();
            if(startMousePosition==s){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit)){
                    UnityRTS u = hit.transform.gameObject.GetComponent<UnityRTS>();
                    building = hit.transform.gameObject.GetComponent<BuildingSpawner>();
                    bool exist = false;
                    foreach(UnityRTS unit in player.playersUnities){
                        if(unit==u)
                            exist = true;
                    }
                    if(u!=null && exist) {
                        foreach(UnityRTS unit in selectedUnities){
                            unit.SetSelectedVisibility(false);
                        }
                        selectedUnities.Clear();
                        selectedUnities.Add(u);
                        u.SetSelectedVisibility(true);
                        update_stats();
                    }
                    if(building!=null)
                        update_stats();
                }
            }

            //Debug.Log(selectedUnities.Count);
            
        }

        if(Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(selectedUnities.Count>0){
                if(Physics.Raycast(ray,out hit)){
                    UnityRTS u = hit.transform.gameObject.GetComponent<UnityRTS>();
                    if(u!=null){
                        foreach(UnityRTS unit in selectedUnities){
                            unit.setCurrentTarget(u.gameObject.transform);
                        }
                    }
                    else{
                        Vector3 path = selectedUnities[0].calculatePath(hit.point);
                        foreach(UnityRTS un in selectedUnities){
                            un.moveToPosiotion(path);
                            un.commanded_to_move = true;
                            Destroy(arrow_temp);
                            arrow_temp = Instantiate(arrows,hit.point - new Vector3(0,0.5f,0),Quaternion.identity);
                            Destroy(arrow_temp,0.5f);
                            un.setCurrentTarget(null);
                        }  
                    }
                }
            }
            if(building!=null){
                if(Physics.Raycast(ray,out hit)){
                    building.setFlag(hit.point);
                }
            }
        }   
    }
    private void update_health_UI(){
        if(selectedUnities.Count>0){
            health.text = ((int)selectedUnities[0].HP).ToString()+"/"+((int)selectedUnities[0].MaxHP).ToString();
            healthbar.fillAmount = selectedUnities[0].HP/selectedUnities[0].MaxHP;
        }
    }
    

    private void update_stats(){
        if(selectedUnities.Count>0){
            stats_panel.gameObject.SetActive(true);
            name_panel.gameObject.SetActive(true);
            attackDamage.text = selectedUnities[0].attackDamage.ToString();
            attackRange.text = selectedUnities[0].attackRange.ToString();
            attackSpeed.text = selectedUnities[0].attackSpeed.ToString();
            defence.text = selectedUnities[0].def.ToString();
            healthRegen.text = selectedUnities[0].healthRegen.ToString();

            unitName.text =selectedUnities[0].Unit_name;
            Icon.sprite = selectedUnities[0].UnitStat.icon;
        }
        else{
            stats_panel.gameObject.SetActive(false);
            name_panel.gameObject.SetActive(false);
        }
        if(building!=null){
            stats_panel.gameObject.SetActive(false);
            name_panel.gameObject.SetActive(true);
            Icon.sprite = building.icon;
            unitName.text = building.building_name;
        }
    }


    public void UnitDied(UnityRTS u){
        selectedUnities.Remove(u);
        player.removeUnit(u);
    }
}
