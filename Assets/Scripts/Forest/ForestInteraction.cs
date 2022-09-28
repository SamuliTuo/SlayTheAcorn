using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestInteraction : MonoBehaviour
{
    public BackgroundControl unstopOnPickup;
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
            if(hit.collider.tag == "Ratamo")
            {
                hit.collider.gameObject.SetActive(false);
            } 
            if(hit.collider.tag == "Acorn")
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/UI/Collect ", gameObject);

                hit.collider.gameObject.SetActive(false);
                PlayerInventory.instance.AddAcorns( Mathf.FloorToInt( 50 +  Mathf.Floor(Random.value*10f)*10f) );
            }
            if(this.unstopOnPickup) this.unstopOnPickup.stopped = false;
        }
    }
}
