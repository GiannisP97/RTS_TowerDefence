using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectUnities : MonoBehaviour
{
    //public Camera cam;
    private Vector3 startposition;
    private Vector3 endPosition;
    private Vector3 startMousePosition;
    // Start is called before the first frame update
    private List<UnityRTS> selectedUnities;
    [SerializeField] private RectTransform selectionAreaTransform;
    void Awake()
    {
        selectedUnities = new List<UnityRTS>();
        selectionAreaTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)){
            selectionAreaTransform.gameObject.SetActive(true);
            startMousePosition  = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit)){
                    startposition = hit.point;
                    Debug.Log(startposition);
            }
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
            selectionAreaTransform.gameObject.SetActive(false);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit)){
                    endPosition = hit.point;
                    Vector3 center  = (endPosition + startposition)/2;
                    Debug.Log(endPosition);
                    Collider[] hitColliders = Physics.OverlapSphere(center,(center-startposition).magnitude);
                    
                    foreach(UnityRTS u in selectedUnities){
                        u.SetSelectedVisibility(false);
                    }
                    selectedUnities.Clear();
                    foreach (var hitCollider in hitColliders)
                    {
                        UnityRTS Unit = hitCollider.GetComponent<UnityRTS>();
                        if( Unit!=null){
                            selectedUnities.Add(Unit);
                            Unit.SetSelectedVisibility(true);
                        }
                    }
                    Debug.Log(selectedUnities.Count);
            }
            
        }

        if(Input.GetMouseButtonDown(1)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit)){
                foreach(UnityRTS u in selectedUnities){
                    u.moveToposition(hit.point);
                }
            }
        }
        
    }
}
