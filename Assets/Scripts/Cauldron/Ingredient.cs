using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public int current = 0;
    public Material ingredientHogweed;
    public Material ingredientLily;
    public Material ingredientFoxGlove;
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
            if(hit.collider.tag == "CauldronIngredient" && hit.collider.gameObject == this.gameObject)
            {
                this.current = (this.current+1)%3;
                Material[] mats = {this.ingredientFoxGlove, this.ingredientHogweed, this.ingredientLily};
                this.GetComponent<Renderer>().material = mats[this.current];
            } 
        }
    }
}
