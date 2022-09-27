using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Touch? t0 = Input.touchCount > 0 ? Input.GetTouch(0) : null;
        Ray? ray = null;
        RaycastHit hit;
        if(Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        }
        if(t0.HasValue && t0.Value.phase == TouchPhase.Ended)
        {
            ray = Camera.main.ScreenPointToRay( t0.Value.position );
        }
        if(ray.HasValue && Physics.Raycast( ray.Value, out hit ))
        {
            Debug.Log("Hitting");
            hit.collider.gameObject.SetActive(false);
        }
    }
}
