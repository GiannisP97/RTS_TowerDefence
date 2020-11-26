using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectUnities : MonoBehaviour
{
    //public Camera cam;
    private Vector3 startposition;
    private Vector3 endPosition;
    private Vector3 startMousePosition;
    private player player;
    // Start is called before the first frame update
    private List<UnityRTS> selectedUnities;
    [SerializeField] private RectTransform selectionAreaTransform;
    void Awake()
    {
        selectedUnities = new List<UnityRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
        player = GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            selectionAreaTransform.gameObject.SetActive(true);
            startMousePosition  = Input.mousePosition;
        }
        if(Input.GetMouseButton(0)){
            Vector3 s =Input.mousePosition;
            Vector3 lowerleft = new Vector3(Mathf.Min(startMousePosition.x,s.x),Mathf.Min(startMousePosition.y,s.y));
            Vector3 upperRight = new Vector3(Mathf.Max(startMousePosition.x,s.x),Mathf.Max(startMousePosition.y,s.y));

            //selectionAreaTransform.pivot = lowerleft;
            selectionAreaTransform.position = lowerleft;
            selectionAreaTransform.localScale = upperRight - lowerleft;
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

            if(startMousePosition==s){
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray,out hit)){
                    UnityRTS u = hit.transform.gameObject.GetComponent<UnityRTS>();
                    if(u!=null){
                        foreach(UnityRTS unit in selectedUnities){
                            unit.SetSelectedVisibility(false);
                        }
                        selectedUnities.Clear();
                        selectedUnities.Add(u);
                        u.SetSelectedVisibility(true);
                    }
                }
            }

            Debug.Log(selectedUnities.Count);
            
        }

        if(Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit)){
                UnityRTS u = hit.transform.gameObject.GetComponent<UnityRTS>();
                if(u!=null){
                    foreach(UnityRTS unit in selectedUnities){
                        unit.setCurrentTarget(u.gameObject.transform);
                    }
                }
                else{
                    foreach(UnityRTS un in selectedUnities){
                        un.moveToposition(hit.point);
                        un.setCurrentTarget(null);
                    }  
                }
            }
        }

                
        
        
    }
}
